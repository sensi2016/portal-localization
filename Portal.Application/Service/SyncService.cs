using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Portal.Application.Interface;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DTO;
using Portal.DTO.User;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.DAL.Extensions;
using Portal.Application.Interface.Base;

namespace Portal.Application.Service
{
    public class SyncService : ISyncService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string[] interNationalCodeVital = new string[] { "271649006", "271650006", "60621009", "78564009", "27113001", "50373000", "737105002", "251075007", "431314004", "251872006", "103579009" };
        private readonly string[] interNationalCodeHistory = new string[] { "33962009", "422625006", "417662000", "161615003", "254292007", "371441004", "86290005" };
        private readonly DbSet<PrescriptionServiceResult> _prescriptionServiceResultRepository;
        private readonly DbSet<PrescriptionDetailDrug> _prescriptionDetailDrugRepository;
        private readonly DbSet<PrescriptionDetailService> _prescriptionDetailService;
        private readonly DbSet<PatientExtraInfo> _patientExtraInfoRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly DbSet<DataTransferConvert> _dataTransferConvert;
        private readonly DbSet<UserRolePermission> _userRolePermission;
        private readonly DbSet<Prescription> _prescriptionRepository;
        private readonly DbSet<DoctorCertificate> _doctorCertificate;
        private readonly DbSet<PatientExtraInfo> _patientExtraInfo;
        private readonly IBasicService<Certificate> _basicService;
        private readonly DbSet<Receptions> _receptionsRepository;
        private readonly IWorkContextService _workContextService;
        private readonly DbSet<Services> _servicesRepository;
        private readonly DbSet<MaritalStatus> _maritalStatus;
        private readonly DbSet<CardCode> _cardCodeRepository;
        private readonly DbSet<UserCardCode> _userCardCode;
        private readonly DbSet<Doctors> _doctorRepository;
        private readonly DbSet<Certificate> _certificate;
        private readonly DbSet<Person> _personRepository;
        private readonly DbSet<Center> _centerRepository;
        private readonly DbSet<BloodGroup> _bloodGroup;
        private readonly DbSet<Users> _userRepository;
        private readonly DbSet<Patient> _patients;
        private readonly DbSet<Section> _section;
        private readonly DbSet<Sex> _sex;
        private readonly DbSet<Rh> _rh;
        public SyncService(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IBasicService<Certificate> basicService,
            IWorkContextService workContextService)
        {
            _unitOfWork = unitOfWork;
            _rh = _unitOfWork.Set<Rh>();
            _sex = _unitOfWork.Set<Sex>();
            _basicService = basicService;
            _sharedLocalizer = sharedLocalizer;
            _section = _unitOfWork.Set<Section>();
            _patients = _unitOfWork.Set<Patient>();
            _workContextService = workContextService;
            _userRepository = _unitOfWork.Set<Users>();
            _bloodGroup = _unitOfWork.Set<BloodGroup>();
            _certificate = _unitOfWork.Set<Certificate>();
            _personRepository = _unitOfWork.Set<Person>();
            _centerRepository = _unitOfWork.Set<Center>();
            _doctorRepository = _unitOfWork.Set<Doctors>();
            _userCardCode = _unitOfWork.Set<UserCardCode>();
            _cardCodeRepository = _unitOfWork.Set<CardCode>();
            _servicesRepository = _unitOfWork.Set<Services>();
            _maritalStatus = _unitOfWork.Set<MaritalStatus>();
            _receptionsRepository = _unitOfWork.Set<Receptions>();
            _patientExtraInfo = _unitOfWork.Set<PatientExtraInfo>();
            _doctorCertificate = _unitOfWork.Set<DoctorCertificate>();
            _prescriptionRepository = _unitOfWork.Set<Prescription>();
            _userRolePermission = _unitOfWork.Set<UserRolePermission>();
            _dataTransferConvert = _unitOfWork.Set<DataTransferConvert>();
            _patientExtraInfoRepository = _unitOfWork.Set<PatientExtraInfo>();
            _prescriptionDetailService = _unitOfWork.Set<PrescriptionDetailService>();
            _prescriptionDetailDrugRepository = _unitOfWork.Set<PrescriptionDetailDrug>();
            _prescriptionServiceResultRepository = _unitOfWork.Set<PrescriptionServiceResult>();
        }


        public async Task<string> SyncData(SyncDto syncDto)
        {
            var deSerializedData = JsonConvert.DeserializeObject<BaseWinAppDto>(syncDto.Data.SerializedData);

            if (deSerializedData.Id != null && syncDto.Data.ServerId != null && deSerializedData.Id != syncDto.Data.ServerId)
            {
                return $"Sync Data error, Server Id: {syncDto.Data.ServerId} doesn't match serialized Data Id: {deSerializedData.Id}";
            }

            var dataIsConvertBeforeId = await CheckDataIsConvertBefore(syncDto.Data.ServerId, syncDto.Data.Table);
            var message = "No such table to sync!";

            switch (syncDto.Data.Table)
            {
                case TablesWinAppEnum.Doctor:
                    message = await DoctorUpsert(syncDto, dataIsConvertBeforeId);
                    break;
                case TablesWinAppEnum.Patient:
                    message = await PatientUpsert(syncDto, dataIsConvertBeforeId);
                    break;
                case TablesWinAppEnum.Prescription:
                    message = await PrescriptionsUpsert(syncDto, dataIsConvertBeforeId);
                    break;
                case TablesWinAppEnum.Drug:
                    message = await DrugUpsert(syncDto, dataIsConvertBeforeId);
                    break;
                case TablesWinAppEnum.VitalSign:
                    message = await VitalSignUpsert(syncDto, dataIsConvertBeforeId);
                    break;
                case TablesWinAppEnum.PatientHistory:
                    message = await PatientHistoryUpsert(syncDto, dataIsConvertBeforeId);
                    break;
                case TablesWinAppEnum.Radiology:
                    message = await RadiologyUpsert(syncDto, dataIsConvertBeforeId);
                    break;
                    //default: throw new SyncDataMismatchException("No such table to sync to!");
            }

            await _unitOfWork.SaveChangesAsync();

            return message;
        }

        public async Task<BaseResponseDto> RegisterDoctorAsync(SyncDoctorDto doctorDto)
        {
            var resultValid = CheckValidate.Valid(new SyncDoctorValidation(_sharedLocalizer, _personRepository, _userRepository), doctorDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            var data = new DoctorWinAppDto
            {
                UserName = doctorDto.UserName,
                Password = doctorDto.Password,
                Mac = doctorDto.Mac,
                Email = doctorDto.Email,
                Phone = doctorDto.Phone
            };

            var exsitDoctor = await _userRepository.Include(n => n.Person).ThenInclude(n => n.Doctors).FirstOrDefaultAsync(n => n.MacAddress == data.Mac);
            if (exsitDoctor != null)
            {
                exsitDoctor.Password = Utilities.ComputeHashSHA256(data.Password);
                exsitDoctor.UserName = data.UserName;

                await _unitOfWork.SaveChangesAsync();

                var doctorid = await _dataTransferConvert.FirstOrDefaultAsync(n => n.TargetTableId == exsitDoctor.Person.Doctors.FirstOrDefault().Id.ToString() && n.TableName == TablesWinAppEnum.Doctor.ToString());
                return new LoginResponseDto { Status = ResponseStatus.Success, Data = new { DoctorId = doctorid.TableId } };
            }

            var result = await DoctorNew(data);
            return new LoginResponseDto { Status = ResponseStatus.Success, Data = result };
        }

        private async Task<long> CheckDataIsConvertBefore(string id, TablesWinAppEnum tableName)
        {
            var item = await _dataTransferConvert
                .Where(n =>
                n.TableId == id &&
                n.TableName == tableName.ToString() &&
                n.UserId == _workContextService.UserId // باید مربوط به دکتر جاری باشد
                )
                .ToListAsync();

            if (item == null)
            {
                return 0;
            }

            return Convert.ToInt64(item.LastOrDefault()?.TargetTableId);
        }
        private async Task DataTransferConvertAdd(string tableId, string tableName, int id)
        {
            await _dataTransferConvert.AddAsync(new DataTransferConvert
            {
                UserId = _workContextService.UserId,
                CreateDate = DateTime.Now,
                TargetTableId = id.ToString(),
                TableId = tableId,
                TableName = tableName
            });
        }
        private async Task<Rh> RhGet(PatientWinAppDto data)
        {
            var r = await _rh.ToListAsync();
            var rhId = r.FirstOrDefault(n => n.Code1.ToLower() == "unknown");

            if (data.RH != null)
            {
                rhId = r.FirstOrDefault(n => n.Code1.ToLower() == Enum.GetName(typeof(RHWinAppEnum), data.RH).ToLower());
            }

            return rhId;
        }
        private async Task<BloodGroup> BloodGet(PatientWinAppDto data)
        {
            var b = await _bloodGroup.ToListAsync();
            var bloodId = b.FirstOrDefault(n => n.Code1?.ToLower() == "unknown");

            if (data.Blood != null)
            {
                bloodId = b.FirstOrDefault(n => n.Code1.ToLower() == Enum.GetName(typeof(BloodWinAppEnum), data.Blood).ToLower());
            }

            return bloodId;
        }
        private async Task<Sex> SexGet(PatientWinAppDto data)
        {
            var s = await _sex.ToListAsync();
            var sexId = s.FirstOrDefault(n => n.Code1.ToLower() == "unknown");

            return s.FirstOrDefault(n => n.Code1.ToLower() == Enum.GetName(typeof(GenderWinAppEnum), data.Gender).ToLower());
        }
        private async Task<MaritalStatus> MaritalStatusGet(PatientWinAppDto data)
        {
            var m = await _maritalStatus.ToListAsync();
            var maritalStatusId = m.FirstOrDefault(n => n.Code1.ToLower() == "other");

            if (data.MartialState != null)
            {
                maritalStatusId = m.FirstOrDefault(n => n.Code1.ToLower() == Enum.GetName(typeof(MartialStateWinAppEnum), data.MartialState).ToLower());
            }

            return maritalStatusId;
        }

        //============================================================================== doctor 
        private async Task<string> DoctorUpsert(SyncDto data, long dataIsConvertBeforeId)
        {
            var deSerializedData = JsonConvert.DeserializeObject<DoctorWinAppDto>(data.Data.SerializedData);
            return await DoctorUpdate(deSerializedData);

            // چون ثبت نام دکتر در قسمت دیگری انجام میشود در اینجا فقط اپدیت دکتر رو خواهیم داشت
            //if (dataIsConvertBeforeId > 0)
            //{
            //    await DoctorUpdate(deSerializedData, dataIsConvertBeforeId);
            //}
            //else
            //{
            //    await DoctorNew(deSerializedData);
            //}
        }
        public async Task<object> DoctorNew(DoctorWinAppDto data)
        {
            //TODO user?
            var user = new Users();
            var person = new Person();
            var doctor = new Doctors();

            //--------------------------------- add person 
            ModelToPerson(data, person);
            await _personRepository.AddAsync(person);
            await _unitOfWork.SaveChangesAsync();

            //--------------------------------- add Center 
            var center = new Center
            {
                IsActive = false,
                CenterTypeId = 4,
                Email = data.Email,
                Phone = data.Phone
            };
            await _centerRepository.AddAsync(center);
            await _unitOfWork.SaveChangesAsync();

            //--------------------------------- add senction
            var section = new Section
            {
                Title = $"DoctorSync-{data.Enname}",
                CenterId = center.Id,
                SectionKindId = 1,
                SectionFieldId = 5,
                IsActive = true
            };

            await _section.AddAsync(section);
            //--------------------------------- add doctor 
            doctor.PersonId = person.Id;
            doctor.CenterId = center.Id;
            await ModelToDoctor(data, doctor);
            await _doctorRepository.AddAsync(doctor);
            await _unitOfWork.SaveChangesAsync();

            if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.Password))
            {
                user = new Users
                {
                    IsActive = true,
                    IsVerify = true,
                    PersonId = person.Id,
                    UserName = data.UserName,
                    MacAddress = data.Mac,
                    Password = Utilities.ComputeHashSHA256(data.Password)
                };

                await _userRepository.AddAsync(user);
            }
            var doctorid = (Guid.NewGuid()).ToString();
            await DataTransferConvertAdd(doctorid, TablesWinAppEnum.Doctor.ToString(), doctor.Id);
            await _unitOfWork.SaveChangesAsync();

            return new
            {
                doctorId = doctorid
            };
        }
        public async Task<string> DoctorUpdateRegister(int userid, DoctorWinAppDto data)
        {

            return "Update Doctor Success";
        }
        public async Task<string> DoctorUpdate(DoctorWinAppDto data)
        {
            var userid = _workContextService.UserId;
            var user = await _userRepository.Where(n => n.Id == userid).FirstOrDefaultAsync();
            if (user == null)
            {
                return "User Not Found";
            }

            var person = await _personRepository
                .Include(p => p.Doctors)
                .Where(n => n.Id == user.PersonId)
                .FirstOrDefaultAsync();

            if (person == null)
            {
                return "Person Not Found";
            }

            var doctor = person.Doctors.FirstOrDefault();
            ModelToPerson(data, person);
            await ModelToDoctor(data, doctor);

            await _unitOfWork.SaveChangesAsync();

            return "Update Doctor Success";
        }

        //============================================================================== patient 
        private async Task<string> PatientUpsert(SyncDto data, long dataIsConvertBeforeId)
        {
            var deSerializedData = JsonConvert.DeserializeObject<PatientWinAppDto>(data.Data.SerializedData);

            if (dataIsConvertBeforeId > 0)
            {
                return await PatientUpdate(deSerializedData, dataIsConvertBeforeId);
            }
            else
            {
                return await PatientNew(deSerializedData);
            }
        }
        public async Task<string> PatientNew(PatientWinAppDto data, long personId = 0)
        {
            //برای زمانی که دکتر دیگری مریض تکراری را سینک میکند 
            if (!string.IsNullOrEmpty(data.NHS_Number))
            {
                var nhs = await _cardCodeRepository.FirstOrDefaultAsync(n => n.HealthNumber == data.NHS_Number);
                if (nhs != null)
                {
                    var userCardCode = await _userCardCode.FirstOrDefaultAsync(n => n.CardCodeId == nhs.Id);
                    if (userCardCode != null)//قبلا این کد توسط دکتر دیگر فعال سازی شده
                    {
                        var _patient = await _userRepository
                            .Include(x => x.Person)
                            .ThenInclude(x => x.Patient)
                            .FirstOrDefaultAsync(x => x.Id == userCardCode.UserId);

                        await DataTransferConvertAdd(data.Id.ToString(), TablesWinAppEnum.Patient.ToString(), _patient.Person.Patient.FirstOrDefault().Id);
                        await _unitOfWork.SaveChangesAsync();

                        return "Add Patient Success";
                    }
                }
            }

            //TODO user id_card --> users.CardCodeId,?
            var person = new Person();
            var patient = new Patient();
            var patientExtraInfo = new PatientExtraInfo();

            //--------------------------------- add person 
            if (personId == 0)
            {
                await ModelToPerson(data, person);
                await _personRepository.AddAsync(person);
                await _unitOfWork.SaveChangesAsync();
                personId = person.Id;
            }

            //--------------------------------- add patient 
            patient.PersonId = (int)personId;
            //patient.FileNo = data.NHS_Number;
            ModelToPatient(data, patient);
            await _patients.AddAsync(patient);

            await CheckNhsActiveBefore(data, personId);
            //--------------------------------- add PatientExtraInfo 
            patientExtraInfo.PatientId = patient.Id;
            await ModelToPatientExtraInfo(data, patientExtraInfo);

            await _patientExtraInfoRepository.AddAsync(patientExtraInfo);
            await DataTransferConvertAdd(data.Id.ToString(), TablesWinAppEnum.Patient.ToString(), patient.Id);

            await _unitOfWork.SaveChangesAsync();

            return "Add Patient Success";

        }

        private async Task CheckNhsActiveBefore(PatientWinAppDto data, long personId)
        {
            if (!string.IsNullOrEmpty(data.NHS_Number))
            {
                var nhs = await _cardCodeRepository.FirstOrDefaultAsync(n => n.HealthNumber == data.NHS_Number);
                if (nhs != null)
                {
                    var userCardCode = await _userCardCode.FirstOrDefaultAsync(n => n.CardCodeId == nhs.Id);

                    //قبلا کاربر فعال سازی را انجام نداده
                    if (userCardCode == null)
                    {
                        var user = new Users
                        {
                            IsActive = true,
                            PersonId = (int)personId,
                            CardCodeId = nhs.Id,
                            UserCardCode = new List<UserCardCode>
                            {
                                new UserCardCode
                                {
                                   CardCodeId = nhs.Id,
                                    IsCurrent = true,
                                    CreateDate = DateTime.Now
                                }
                            }
                        };

                        await _userRepository.AddAsync(user);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        // یوزر جاری 
                        var userId = userCardCode.UserId.GetValueOrDefault();
                        var user = await _userRepository
                            .Include(x => x.Person)
                            .ThenInclude(x => x.Patient)
                            .ThenInclude(n => n.PatientExtraInfo)
                            .FirstOrDefaultAsync(x => x.Id == userId);

                        if (user.Person.Patient.TryAny())
                        {
                            _patientExtraInfoRepository.RemoveRange(user.Person.Patient.First().PatientExtraInfo);
                            _patients.Remove(user.Person.Patient.First());
                        }


                        // آپدیت فایل منیجر
                        // هنوز انجام نشده

                        user.PersonId = (int)personId;
                        user.IsSync = true;
                        _userRepository.Update(user);

                        _personRepository.Remove(user.Person);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
        private async Task CheckNhsActiveBefore_Update(PatientWinAppDto data, long personId)
        {
            if (!string.IsNullOrEmpty(data.NHS_Number))
            {
                var nhs = await _cardCodeRepository.FirstOrDefaultAsync(n => n.HealthNumber == data.NHS_Number);
                if (nhs != null)
                {
                    var userCardCode = await _userCardCode.FirstOrDefaultAsync(n => n.CardCodeId == nhs.Id);

                    var currenntUser = await _userRepository.FirstOrDefaultAsync(n => n.PersonId == personId);

                    if (userCardCode.UserId == currenntUser.Id)
                    {
                        return;
                    }

                    //قبلا کاربر فعال سازی را انجام نداده
                    if (userCardCode == null)
                    {
                        var ucc = new UserCardCode
                        {
                            CardCodeId = nhs.Id,
                            IsCurrent = true,
                            UserId = currenntUser.Id,
                            CreateDate = DateTime.Now
                        };

                        await _userCardCode.AddAsync(ucc);
                    }
                    else
                    {
                        // یوزر جاری 
                        var userId = userCardCode.UserId.GetValueOrDefault();
                        var user = await _userRepository
                            .Include(x => x.Person)
                            .ThenInclude(x => x.Patient)
                            .ThenInclude(n => n.PatientExtraInfo)
                            .FirstOrDefaultAsync(x => x.Id == userId);

                        if (user.Person.Patient.TryAny())
                        {
                            _patientExtraInfoRepository.RemoveRange(user.Person.Patient.First().PatientExtraInfo);
                            _patients.Remove(user.Person.Patient.First());
                        }


                        // آپدیت فایل منیجر
                        // هنوز انجام نشده

                        user.PersonId = (int)personId;
                        user.IsSync = true;
                        _userRepository.Update(user);

                        _personRepository.Remove(user.Person);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string> PatientUpdate(PatientWinAppDto data, long personId)
        {
            //---------------------------------------- person
            var person = await _personRepository
                .Where(n => n.Id == personId)
                .FirstOrDefaultAsync();

            if (person == null)
            {
                return await PatientNew(data);
            }

            //---------------------------------------- patient
            var patient = await _patients
                .Where(n => n.PersonId == personId)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                await PatientNew(data, personId);
            }

            await ModelToPerson(data, person);
            ModelToPatient(data, patient);

            await CheckNhsActiveBefore_Update(data, personId);

            //---------------------------------------- extraInfo
            var patientExtraInfo = await _patientExtraInfoRepository
               .Where(n => n.PatientId == patient.Id)
               .FirstOrDefaultAsync();

            if (patientExtraInfo == null)
            {
                patientExtraInfo = new PatientExtraInfo
                {
                    PatientId = patient.Id
                };
                await ModelToPatientExtraInfo(data, patientExtraInfo);
                await _patientExtraInfoRepository.AddAsync(patientExtraInfo);
            }
            else
            {
                await ModelToPatientExtraInfo(data, patientExtraInfo);
            }

            await _unitOfWork.SaveChangesAsync();

            return "Update Patient Success";
        }

        //============================================================================== Prescriptions 
        private async Task<string> PrescriptionsUpsert(SyncDto data, long dataIsConvertBeforeId)
        {
            var deSerializedData = JsonConvert.DeserializeObject<PrescriptionWinAppDto>(data.Data.SerializedData);

            if (dataIsConvertBeforeId > 0)
            {
                return await PrescriptionsUpdate(deSerializedData, dataIsConvertBeforeId);
            }
            else
            {
                var result = await PrescriptionsNew(deSerializedData);
                return result.message;
            }
        }
        public async Task<(string message, int id)> PrescriptionsNew(PrescriptionWinAppDto data)
        {
            var patient = await _dataTransferConvert.FirstOrDefaultAsync(n => n.TableId == data.PatientId && n.TableName == TablesWinAppEnum.Patient.ToString() && n.UserId == _workContextService.UserId);

            if (!int.TryParse(patient?.TargetTableId, out int patientId))
            {
                return ("Patient Not Found", 0);
            }

            var doctorId = await GetDoctorId(data.DoctorId);
            if (doctorId == 0)
            {
                return ("Doctor Not Found", 0);
            }
            //--------------------------------- add reception 
            var tempReception = new Receptions
            {
                ReceptionDate = data.Date,
                PatientId = patientId,
                DoctorId = doctorId,
                Note = data.Notes,
                SectionId = 1
            };

            await _receptionsRepository.AddAsync(tempReception);
            await _unitOfWork.SaveChangesAsync();

            //--------------------------------- add Prescriptions 
            var tempPrescription = new Prescription
            {
                ReceptionId = tempReception.Id,
                CreateDate = data.Date,
                DoctorId = doctorId,
                Note = data.Notes
            };

            await _prescriptionRepository.AddAsync(tempPrescription);
            await _unitOfWork.SaveChangesAsync();

            await DataTransferConvertAdd(data.Id, TablesWinAppEnum.Prescription.ToString(), (int)tempPrescription.Id);
            await _unitOfWork.SaveChangesAsync();

            return ("Add Prescription Success", (int)tempPrescription.Id);

        }
        public async Task<string> PrescriptionsUpdate(PrescriptionWinAppDto data, long id)
        {
            var temp = await _prescriptionRepository
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();

            if (temp == null)
            {
                var result = await PrescriptionsNew(data);
                return result.message;
            }

            temp.Note = data.Notes;
            await _unitOfWork.SaveChangesAsync();

            return "Update Prescriptions Success";
        }

        //============================================================================== Drug 
        private async Task<string> DrugUpsert(SyncDto data, long dataIsConvertBeforeId)
        {
            var deSerializedData = JsonConvert.DeserializeObject<DrugWinAppDto>(data.Data.SerializedData);

            if (dataIsConvertBeforeId > 0)
            {
                return await DrugUpdate(deSerializedData, dataIsConvertBeforeId);
            }
            else
            {
                return await DrugNew(deSerializedData);
            }
        }
        public async Task<string> DrugNew(DrugWinAppDto data)
        {
            var prescription = await _dataTransferConvert.Where(n => n.TableId == data.PrescriptionId && n.TableName == TablesWinAppEnum.Prescription.ToString() && n.UserId == _workContextService.UserId).ToListAsync();

            if (!int.TryParse(prescription.LastOrDefault()?.TargetTableId, out int prescriptionId))
            {
                return "Prescription Not Found";
            }

            //--------------------------------- add drug 
            var temp = new PrescriptionDetailDrug
            {
                PrescriptionId = prescriptionId
            };

            ModelToDrug(data, temp);
            await _prescriptionDetailDrugRepository.AddAsync(temp);
            await _unitOfWork.SaveChangesAsync();

            await DataTransferConvertAdd(data.Id.ToString(), TablesWinAppEnum.Drug.ToString(), (int)temp.Id);
            await _unitOfWork.SaveChangesAsync();

            return "Add Drug Success";

        }
        public async Task<string> DrugUpdate(DrugWinAppDto data, long id)
        {
            var temp = await _prescriptionDetailDrugRepository
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();

            if (temp == null)
            {
                return "Drug Not Found";
            }

            ModelToDrug(data, temp);
            await _unitOfWork.SaveChangesAsync();

            return "Update Drug Success";
        }

        //============================================================================== VitalSign 
        private async Task<string> VitalSignUpsert(SyncDto data, long dataIsConvertBeforeId)
        {
            var deSerializedData = JsonConvert.DeserializeObject<VitalSignWinAppDto>(data.Data.SerializedData);

            if (dataIsConvertBeforeId > 0)
            {
                return await VitalSignUpdate(deSerializedData, dataIsConvertBeforeId);
            }
            else
            {
                return await VitalSignNew(deSerializedData);
            }
        }
        public async Task<string> VitalSignNew(VitalSignWinAppDto data)
        {
            long prescriptionId = 0;
            var prescription = await _prescriptionRepository.FirstOrDefaultAsync(n => n.PrescriptionDate == data.Date);

            if (prescription == null)
            {
                var prescriptionNew = await PrescriptionsNew(new PrescriptionWinAppDto
                {
                    Id = $"CreatForVitalSing:{data.Id}",
                    PatientId = data.PatientId,
                    DoctorId = data.DoctorId,
                    Notes = data.Notes,
                    Date = data.Date
                });

                prescriptionId = prescriptionNew.id;
            }
            else
            {
                prescriptionId = prescription.Id;
            }

            if (prescriptionId == 0)
            {
                return "Prescription Not Found";
            }
            //--------------------------------- Get Service
            List<PrescriptionServiceResult> listAdd = await ModelToVitalSign(data, prescriptionId);

            await _prescriptionServiceResultRepository.AddRangeAsync(listAdd);

            await DataTransferConvertAdd(data.Id, TablesWinAppEnum.VitalSign.ToString(), (int)prescriptionId);
            await _unitOfWork.SaveChangesAsync();

            return "Add Vital singn Success";
        }

        public async Task<string> VitalSignUpdate(VitalSignWinAppDto data, long id)// this id = prescriptionId
        {
            var removeList = await _prescriptionServiceResultRepository
                .Where(n => n.PrescriptionId == id && interNationalCodeVital.Contains(n.ServiceId.ToString()))
                .ToListAsync();

            // حذف رکوردهای قبلی ویتال
            _prescriptionServiceResultRepository.RemoveRange(removeList);
            // دریافت سرویس های جدید ویتال
            List<PrescriptionServiceResult> listAdd = await ModelToVitalSign(data, id);

            await _prescriptionServiceResultRepository.AddRangeAsync(listAdd);
            await _unitOfWork.SaveChangesAsync();

            return "Update Vital singn Success";
        }

        //============================================================================== PatientHistory 
        private async Task<string> PatientHistoryUpsert(SyncDto data, long dataIsConvertBeforeId)
        {
            var deSerializedData = JsonConvert.DeserializeObject<PatientHistoryWinAppDto>(data.Data.SerializedData);

            if (dataIsConvertBeforeId > 0)
            {
                return await PatientHistoryUpdate(deSerializedData, dataIsConvertBeforeId);
            }
            else
            {
                return await PatientHistoryNew(deSerializedData);
            }
        }
        public async Task<string> PatientHistoryNew(PatientHistoryWinAppDto data)
        {
            long prescriptionId = 0;
            var prescription = await _prescriptionRepository.FirstOrDefaultAsync(n => n.PrescriptionDate == data.Date);

            if (prescription == null)
            {
                var prescriptionNew = await PrescriptionsNew(new PrescriptionWinAppDto
                {
                    Id = $"CreatForPatientHistory:{data.Id}",
                    PatientId = data.PatientId,
                    DoctorId = data.DoctorId,
                    Date = data.Date,
                    Notes = ""
                });

                prescriptionId = prescriptionNew.id;
            }
            else
            {
                prescriptionId = prescription.Id;
            }

            if (prescriptionId == 0)
            {
                return "Prescription Not Found";
            }

            //--------------------------------- Get Service
            List<PrescriptionServiceResult> listAdd = await ModelToPatientHistory(data, prescriptionId);

            await _prescriptionServiceResultRepository.AddRangeAsync(listAdd);

            await DataTransferConvertAdd(data.Id, TablesWinAppEnum.PatientHistory.ToString(), (int)prescriptionId);
            await _unitOfWork.SaveChangesAsync();

            return "Add Patient History Success";
        }
        public async Task<string> PatientHistoryUpdate(PatientHistoryWinAppDto data, long id)// this id = prescriptionId
        {

            var removeList = await _prescriptionServiceResultRepository
                .Where(n => n.PrescriptionId == id && interNationalCodeHistory.Contains(n.ServiceId.ToString()))
                .ToListAsync();

            // حذف رکوردهای قبلی ویتال
            _prescriptionServiceResultRepository.RemoveRange(removeList);
            // دریافت سرویس های جدید ویتال
            List<PrescriptionServiceResult> listAdd = await ModelToPatientHistory(data, id);

            await _prescriptionServiceResultRepository.AddRangeAsync(listAdd);
            await _unitOfWork.SaveChangesAsync();

            return "Update Patient History Success";
        }

        //============================================================================== Radiology 
        private async Task<string> RadiologyUpsert(SyncDto data, long dataIsConvertBeforeId)
        {
            var deSerializedData = JsonConvert.DeserializeObject<RadiologyWinAppDto>(data.Data.SerializedData);

            if (dataIsConvertBeforeId > 0)
            {
                return await RadiologyUpdate(deSerializedData, dataIsConvertBeforeId);
            }
            else
            {
                return await RadiologyNew(deSerializedData);
            }
        }
        public async Task<string> RadiologyNew(RadiologyWinAppDto data)
        {
            long prescriptionId = 0;
            var prescription = await _prescriptionRepository.FirstOrDefaultAsync(n => n.PrescriptionDate == data.Date);

            if (prescription == null)
            {
                var prescriptionNew = await PrescriptionsNew(new PrescriptionWinAppDto
                {
                    Id = $"CreatForRadiology:{data.Id}",
                    PatientId = data.PatientId,
                    Date = data.Date ?? DateTime.Now,
                    DoctorId = data.DoctorId,
                    Notes = ""
                });

                prescriptionId = prescriptionNew.id;
            }
            else
            {
                prescriptionId = prescription.Id;
            }

            if (prescriptionId == 0)
            {
                return "Prescription Not Found";
            }

            var service = await _servicesRepository.Where(n => n.LocalCode == "rad").FirstOrDefaultAsync();

            var temp = new PrescriptionDetailService
            {
                PrescriptionId = prescriptionId,
                ServiceId = service.Id
            };

            var note = "";
            if (!string.IsNullOrEmpty(data.Type))
            {
                note += $"\"Name\":\"{data.Type}\",";
            }
            if (!string.IsNullOrEmpty(data.Report))
            {
                note += $"\"Note\":\"{data.Report}\",";
            }
            if (!string.IsNullOrEmpty(note))
            {
                note = $"{{{note.Trim(',')}}}";
            }

            if (note == null)
            {
                return "Radiology data was Empty";
            }

            temp.Note = note;

            await _prescriptionDetailService.AddAsync(temp);

            await DataTransferConvertAdd(data.Id, TablesWinAppEnum.Radiology.ToString(), (int)prescriptionId);
            await _unitOfWork.SaveChangesAsync();

            return "Add Radiology Success";
        }
        public async Task<string> RadiologyUpdate(RadiologyWinAppDto data, long id)// this id = prescriptionId
        {

            var temp = await _prescriptionDetailService
                .Where(n => n.PrescriptionId == id)
                .FirstOrDefaultAsync();

            temp.Note = "";
            if (!string.IsNullOrEmpty(data.Type))
            {
                temp.Note += $"\"Name\":\"{data.Type}\",";
            }
            if (!string.IsNullOrEmpty(data.Report))
            {
                temp.Note += $"\"Note\":\"{data.Report}\",";
            }
            if (!string.IsNullOrEmpty(temp.Note))
            {
                temp.Note = $"{{{temp.Note.Trim(',')}}}";
            }

            if (temp == null)
            {
                return "Radiology data was Empty";
            }

            await _unitOfWork.SaveChangesAsync();

            return "Update Radiology Success";
        }

        #region mapModel

        private void ModelToPerson(DoctorWinAppDto data, Person person)
        {
            person.LatinName = data.Enname;
            person.FirstName = data.ArName;
            person.Address = data.Address;
            person.Phone = data.Phone;
            person.Email = data.Email;
            person.Note = data.Enspecialty;
        }

        private async Task ModelToPerson(PatientWinAppDto data, Person person)
        {
            MaritalStatus maritalStatusId = await MaritalStatusGet(data);
            Sex sexId = await SexGet(data);

            person.Phone = data.Phone;
            person.FirstName = data.Name;
            person.Address = data.Address;
            person.BirthDate = data.Birthday;
            person.MaritalStatusId = maritalStatusId?.Id;
            person.SexId = sexId?.Id;
        }

        private void ModelToPatient(PatientWinAppDto data, Patient patient)
        {
            int.TryParse(data.NHS_Number, out int nhsNo);

            patient.Hisno = nhsNo;
        }

        private async Task ModelToPatientExtraInfo(PatientWinAppDto data, PatientExtraInfo person)
        {
            if (person == null)
            {
                return;
            }
            //TODO json Format ???
            BloodGroup bloodId = await BloodGet(data);
            Rh rhId = await RhGet(data);

            person.Note = "";
            if (!string.IsNullOrEmpty(data.chronic))
            {
                person.Note += $"\"Chronic\": \"{data.chronic}\",";
            }
            if (!string.IsNullOrEmpty(data.note))
            {
                person.Note += $"\"Note\": \"{data.note}\",";
            }
            if (!string.IsNullOrEmpty(data.Allergy))
            {
                person.Note += $"\"Allergy\": \"{data.Allergy}\",";
            }
            if (!string.IsNullOrEmpty(data.Work))
            {
                person.Note += $"\"Work\": \"{data.Work}\",";
            }
            if (!string.IsNullOrEmpty(person.Note))
            {
                person.Note = $"{{{person.Note.Trim(',')}}}";
            }


            person.RhId = rhId?.Id;
            person.BloodGroupId = bloodId?.Id;
            person.IsDrinking = data.Drinking;
            person.IsSmoking = data.Smoking;
        }

        private async Task ModelToDoctor(DoctorWinAppDto data, Doctors doctor)
        {
            doctor.Note = "";
            doctor.CostVisit = (decimal)data.Visitfee;

            if (!string.IsNullOrEmpty(data.Enspecialty))
            {
                doctor.Note += $"\"Enspecialty\":\"{data.Enspecialty}\",";
            }

            if (doctor.Note != "")
            {
                doctor.Note = $"{{{doctor.Note.Trim(',')}}}";
            }
            if (!string.IsNullOrEmpty(data.Encertificates_1))
            {
                doctor.Note += $"\"Encertificates_1\":\"{data.Encertificates_1}\",";
            }
            if (!string.IsNullOrEmpty(data.Encertificates_2))
            {
                doctor.Note += $"\"Encertificates_2\":\"{data.Encertificates_2}\",";
            }
            if (!string.IsNullOrEmpty(data.Encertificates_3))
            {
                doctor.Note += $"\"Encertificates_3\":\"{data.Encertificates_3}\",";
            }
            if (!string.IsNullOrEmpty(data.Arcertificates_1))
            {
                doctor.Note += $"\"Arcertificates_1\":\"{data.Arcertificates_1}\",";
            }
            if (!string.IsNullOrEmpty(data.Arcertificates_2))
            {
                doctor.Note += $"\"Arcertificates_2\":\"{data.Arcertificates_2}\",";
            }
            if (!string.IsNullOrEmpty(data.Arcertificates_3))
            {
                doctor.Note += $"\"Arcertificates_3\":\"{data.Arcertificates_3}\",";
            }

            if (doctor.Id > 0)// زمان ویرایش ، سرتفیکیت های قبلی حذف میشوند 
            {
                var doctorCertificates = await _doctorCertificate.Where(n => n.DoctorId == doctor.Id).ToListAsync();
                _doctorCertificate.RemoveRange(doctorCertificates);
            }

            doctor.DoctorCertificate = new List<DoctorCertificate>();

            var cer1 = await AddCertificate(data.Arcertificates_1, data.Encertificates_1);
            if (cer1 != null)
            {
                AddCertificateToDoctor(doctor, (int)cer1, data.Arcertificates_1, data.Encertificates_1);
            }

            var cer2 = await AddCertificate(data.Arcertificates_2, data.Encertificates_2);
            if (cer2 != null)
            {
                AddCertificateToDoctor(doctor, (int)cer2, data.Arcertificates_2, data.Encertificates_2);
            }

            var cer3 = await AddCertificate(data.Arcertificates_3, data.Encertificates_3);
            if (cer3 != null)
            {
                AddCertificateToDoctor(doctor, (int)cer3, data.Arcertificates_3, data.Encertificates_3);
            }
        }

        private static void AddCertificateToDoctor(Doctors doctor,int id, string ar, string en)
        {
            if (id == 0)
            {
                doctor.DoctorCertificate.Add(
                        new DoctorCertificate
                        {
                            Certificate = new Certificate
                            {
                                Title = ar,
                                TitleLang2 = en,
                                IsActive = true
                            }
                        });
            }
            else
            {
                doctor.DoctorCertificate.Add(
                       new DoctorCertificate
                       {
                           CertificateId = id
                       });
            }
        }

        private async Task<int?> AddCertificate(string ar, string en)
        {
            if (!string.IsNullOrEmpty(ar) || !string.IsNullOrEmpty(en))
            {
                var cer = await _certificate.Where(n => n.Title == ar || n.TitleLang2 == en).FirstOrDefaultAsync();
                if (cer == null)
                {
                    return 0;
                }

                return cer.Id;
            }

            return null;
        }

        private static void ModelToDrug(DrugWinAppDto data, PrescriptionDetailDrug temp)
        {
            temp.Note = "";
            if (!string.IsNullOrEmpty(data.Name))
            {
                temp.Note = $"\"DrugName\":\"{data.Name}\",";
            }
            if (!string.IsNullOrEmpty(data.Details))
            {
                temp.Note += $"\"Details\":\"{data.Details}\",";
            }
            if (!string.IsNullOrEmpty(data.Notes))
            {
                temp.Note += $"\"Notes\":\"{data.Notes}\",";
            }
            if (!string.IsNullOrEmpty(data.Meal))
            {
                temp.Note += $"\"Meal\":\"{data.Meal}\",";
            }

            if (!string.IsNullOrEmpty(temp.Note))
            {
                temp.Note = $"{{{temp.Note.TrimEnd(',')}}}";
            }
        }

        private async Task<List<PrescriptionServiceResult>> ModelToVitalSign(VitalSignWinAppDto data, long prescriptionId)
        {
            var services = await _servicesRepository
                .Where(n => interNationalCodeVital.Contains(n.InterNationalCode))
                .Select(n => new
                {
                    n.Id,
                    n.InterNationalCode
                }).ToListAsync();

            //--------------------------------- add VitalSign 
            var bp = data.BP.Split('/');
            var listAdd = new List<PrescriptionServiceResult>();
            foreach (var item in interNationalCodeVital)
            {
                var result = "";
                switch (item)
                {
                    case "271649006": result = bp[0]; break;
                    case "271650006": result = bp.Length > 1 ? bp[1] : ""; break;
                    case "60621009": result = data.BMI; break;
                    case "78564009": result = data.PR; break;
                    case "27113001": result = data.WT; break;
                    case "50373000": result = data.HT; break;
                    case "737105002": result = data.GFR; break;
                    case "251075007": result = data.Mean; break;
                    case "431314004": result = data.SPO; break;
                    case "251872006": result = data.Creatinine; break;
                    case "103579009": result = data.SkinRace; break;
                        //case "33962009": result = data.cc; break;
                        //case "422625006": result = data.phi; break;
                        //case "417662000": result = data.pmh; break;
                        //case "161615003": result = data.PSH; break;
                        //case "254292007": result = data.CancerStage; break;
                        //case "371441004": result = data.Cancer Type; break;
                        //case "86290005": result = data.rr; break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    continue;
                }

                var _serviceId = services.FirstOrDefault(n => n.InterNationalCode == item)?.Id;
                if (_serviceId != null)
                {
                    listAdd.Add(new PrescriptionServiceResult { PrescriptionId = prescriptionId, CreateDate = data.Date, Result = result, ServiceId = _serviceId });
                }
            }

            var note = "";
            if (!string.IsNullOrEmpty(data.Notes))
            {
                note += $"\"Notes\":\"{data.Notes}\",";
            }
            if (!string.IsNullOrEmpty(note))
            {
                listAdd.Add(new PrescriptionServiceResult { PrescriptionId = prescriptionId, CreateDate = data.Date, Result2 = $"{{{note.TrimEnd(',')}}}", ServiceId = 9328 });
            }
            return listAdd;
        }

        private async Task<List<PrescriptionServiceResult>> ModelToPatientHistory(PatientHistoryWinAppDto data, long prescriptionId)
        {
            var services = await _servicesRepository
                .Where(n => interNationalCodeHistory.Contains(n.InterNationalCode))
                .Select(n => new
                {
                    n.Id,
                    n.InterNationalCode
                }).ToListAsync();

            var listAdd = new List<PrescriptionServiceResult>();
            foreach (var item in interNationalCodeHistory)
            {
                var result = "";
                switch (item)
                {
                    case "33962009": result = data.CC; break;
                    case "422625006": result = data.PHI; break;
                    case "417662000": result = data.PMH; break;
                    case "161615003": result = data.PSH; break;
                    case "254292007": result = data.CancerStage; break;
                    case "371441004": result = data.CancerType; break;
                    case "86290005": result = data.RR; break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    continue;
                }

                var _serviceId = services.FirstOrDefault(n => n.InterNationalCode == item)?.Id;
                if (_serviceId != null)
                {
                    listAdd.Add(new PrescriptionServiceResult { PrescriptionId = prescriptionId, CreateDate = data.Date, Result = result, ServiceId = _serviceId });
                }
            }

            var note = "";
            if (!string.IsNullOrEmpty(data.Note))
            {
                note += $"\"Note\":\"{data.Note}\",";
            }
            if (!string.IsNullOrEmpty(data.II))
            {
                note += $"\"ii\":\"{data.II}\",";
            }
            if (!string.IsNullOrEmpty(data.POM))
            {
                note += $"\"POM\":\"{data.POM}\",";
            }
            if (!string.IsNullOrEmpty(note))
            {
                listAdd.Add(new PrescriptionServiceResult { PrescriptionId = prescriptionId, CreateDate = data.Date, Result2 = $"{{{note.TrimEnd(',')}}}", ServiceId = 9328 });
            }

            return listAdd;
        }

        private async Task<int> GetDoctorId(string doctorId)
        {
            var doctor = await _dataTransferConvert.FirstOrDefaultAsync(n => n.TableId == doctorId && n.TableName == TablesWinAppEnum.Doctor.ToString());

            int.TryParse(doctor?.TargetTableId, out int id);

            return id;

        }
        #endregion

    }
}
