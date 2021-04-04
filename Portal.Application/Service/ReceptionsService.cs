using His.Reception.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Portal.Application.Interface;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.DTO.Covid;
using Portal.DTO.Message;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Portal.Application.Mapper;

namespace Portal.Application.Service
{
    public class ReceptionsService : IReceptionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Receptions> _receptionsRepository;
        private readonly DbSet<Patient> _patientRepository;
        private readonly DbSet<Answer1> _answerRepository;
        private readonly DbSet<Person> _personRepository;
        private readonly DbSet<Users> _usersRepository;
        private readonly DbSet<Section> _sectionRepository;
        private readonly DbSet<MobileActivation> _mobileActivationRepository;
        private readonly DbSet<ReceptionService> _receptionServiceRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IFileManagerService _fileManagerService;
        private readonly IFilesService _filesService;
        private readonly ISmsService _smsService;
        private readonly DbSet<Services> _serviceRepository;
        private readonly IConfiguration _configuration;
        private readonly IWorkContextService _workContext;

        public ReceptionsService(IConfiguration configuration, IWorkContextService workContextService, IFilesService filesService, IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer, IFileManagerService fileManagerService, ISmsService smsService)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _receptionsRepository = unitOfWork.Set<Receptions>();
            _patientRepository = unitOfWork.Set<Patient>();
            _personRepository = unitOfWork.Set<Person>();
            _mobileActivationRepository = unitOfWork.Set<MobileActivation>();
            _sectionRepository = unitOfWork.Set<Section>();
            _usersRepository = unitOfWork.Set<Users>();
            _serviceRepository = unitOfWork.Set<Services>();
            _answerRepository = unitOfWork.Set<Answer1>();
            _receptionServiceRepository = unitOfWork.Set<ReceptionService>();
            _fileManagerService = fileManagerService;
            _smsService = smsService;
            _configuration = configuration;
            _filesService = filesService;
            _workContext = workContextService;
        }

        public async Task<BaseResponseDto> AddCovid(ReceptionCovidDto receptionCovidDto)
        {
            var resultValid = CheckValidate.Valid<ReceptionCovidDto>(new ReceptionCovidValidation(_sharedLocalizer), receptionCovidDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }
            //  اگر پشن ایدی داشت
            //اطلاعات پشین پرسن اپدیت میکنیم

            _unitOfWork.Databases.BeginTransaction();

            Patient curPatient = null;
            int? parentId = null;

            if (receptionCovidDto.RelationId == (int)RelationShipEnum.MySelf)
            {
                curPatient = await _patientRepository.Where(d => d.Person.Mobile == receptionCovidDto.Mobile)
                                                  .Include(d => d.PatientExtraInfo)
                                                  .Include(d => d.Person)
                                                  .FirstOrDefaultAsync();
            }
            else
            {
                //اگر شخص دیگیری بود و ثبت نام نشده بود باید برای اون اطلاعات پرسنلی ثبت کنیم
                //که فقط شماره تلفن ثبت شده و ایدی پرسن در داخل ریسپشن به عنوان پرنت ذخیره می شود

                //curPatient = await _patientRepository.Where(d => d.Person.Mobile == receptionCovidDto.Mobile)
                //                                .Include(d => d.Person)
                //                              .FirstOrDefaultAsync();
                // برای اینکه بیماره پدر ویرایش نود جاری را نمیگیریم
                //و همیشه تکراری ثبت می کنیم

                parentId = curPatient?.PersonId;

                if (curPatient == null)
                {
                    Patient patient = new Patient();
                    Person person = new Person();
                    //شماره موبایلهم تداخل میخورد مشکل ساز می شود زمان گت بای زدن از طریق شماره موبایل
                    //person.Mobile = receptionCovidDto.Mobile;
                    patient.Person = person;

                    _patientRepository.Add(patient);
                    await _unitOfWork.SaveChangesAsync();
                    parentId = patient.PersonId;
                }

            }

            var map = ReceptionCovidMapper.Map(receptionCovidDto, curPatient);
            map.ParentId = parentId;
            map.SectionId = _workContext.SectionId;
            _receptionsRepository.Add(map);

            //map.ParentId = parentId;

            var maxReceptionId = (await MaxReceptionId()).Data;
            var maxIntenralId = (await MaxInternalId()).Data;
            long receptionId = +(long)maxReceptionId;
            int intenralId = +(int)maxIntenralId;

            map.ReceptionId = ++receptionId;

            if (_unitOfWork.Entry(map.Patient).State == EntityState.Added)
                map.Patient.InternalId = ++intenralId;

            map.ReceptionDate = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();



            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Databases.CommitTransaction();


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["ReceptionCovidForm.Response.AddSuccess"]//+
            };
        }

        public async Task<BaseResponseDto> Get(long id)
        {
            var cur = await _receptionsRepository.Where(d => d.Id == id)
                                                .Include(d => d.Patient.Person)
                                                .Include(d => d.Patient.PatientExtraInfo)
                                                .Include(d => d.SpecialIllnessReception)
                                                .Include(d => d.ReceptionAnswer)
                                                .Include(d => d.ReceptionSign)
                                                .Include(d => d.Relation)
                                                .FirstOrDefaultAsync();

            var map = ReceptionCovidMapper.Map(cur);

            return new BaseResponseDto
            {
                Data = map,
                Status = ResponseStatus.Success
            };
        }

        public async Task<ListResponseDto> Search(FilterReceptionCovidDto dto)
        {
            var queryable = _receptionsRepository.OrderByDescending(d => d.Id)
                .If(!string.IsNullOrEmpty(dto.Mobile), x => x.Where(d => EF.Functions.Like(d.Patient.Person.Mobile, $"%{dto.Mobile}%")))
                .If(!string.IsNullOrEmpty(dto.Name), x => x.Where(d => EF.Functions.Like(d.Patient.Person.FirstName + " " + d.Patient.Person.FatherName + " " + d.Patient.Person.GrandFatherName + " " + d.Patient.Person.LastName, $"%{dto.Name}%")))
                .AsQueryable();

            var count = await queryable.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var receptions = await queryable.ToPagedQuery(dto).Select(ReceptionCovidMapper.MapList).AsNoTracking().ToListAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = receptions
            };

        }

        public async Task<BaseResponseDto> SetAnswer(SetAnswerCovidDto setAnswerCovidDto)
        {
            var curReception = await _receptionsRepository.Where(d => d.Id == setAnswerCovidDto.ReceptionId).FirstOrDefaultAsync();

            curReception.IsResult = setAnswerCovidDto.IsResult;
            curReception.ResultNote = setAnswerCovidDto.ResultNote;

            _receptionsRepository.Update(curReception);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["SetAnswerCovid.Response.SetAnswerSuccess"]//+
            };
        }

        public async Task<BaseResponseDto> BaseInfoAnswer(long receptionId)
        {
            var map = await _receptionsRepository.Where(d => d.Id == receptionId).Select(ReceptionCovidMapper.MapBaseInfoAnswer).FirstOrDefaultAsync();

            var file = await _filesService.GetFilesByFileGroupId(1, nameof(Person), map.PersonId.ToString());

            map.FileId = file.OrderByDescending(d => d.Id).Select(d => d.RefferKey).FirstOrDefault();

            return new BaseResponseDto
            {
                Data = map,
                Status = ResponseStatus.Success
            };
        }

        #region method

        public async Task<BaseResponseDto> MaxReceptionId()
        {
            var maxReceptionCode = await _receptionsRepository.MaxAsync(r => r.ReceptionId);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = maxReceptionCode.GetValueOrDefault()
            };
        }

        public async Task<BaseResponseDto> MaxInternalId()
        {
            var maxInternalId = await _patientRepository.MaxAsync(r => r.InternalId);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = maxInternalId.GetValueOrDefault()
            };

        }

        public async Task<BaseResponseDto> GetResultByUserId(long userId)
        {
            var user = await _usersRepository.Where(d => d.Id == userId)
                .Include(d => d.Person)
                .FirstOrDefaultAsync();


            var lst = await _receptionsRepository.Where(d => d.Patient.Person.Mobile == user.Person.Mobile || d.ParentId == user.PersonId)
                .Select(g => new
                {
                    g.Id,
                    g.RelationId,
                    Date = g.ReceptionDate.ToDateStringTry(),
                    Center = g.Section.Center.Title
                })
                .ToListAsync();


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst
            };
        }
        #endregion

        #region covid airport

        public async Task<BaseResponseDto> AddCovidAirPort(ReceptionCovidAirPortDto receptionCovidAirPortDto)
        {
            var resultValid = CheckValidate.Valid<ReceptionCovidAirPortDto>(new ReceptionCovidAirPortValidation(_sharedLocalizer), receptionCovidAirPortDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            _unitOfWork.Databases.BeginTransaction();

            //  var sectionId = await _sectionRepository.Where(d => d.CenterId == receptionCovidAirPortDto.CenterId).Select(g=>g.Id).FirstOrDefaultAsync();

            Patient curPatient = null;
            int? parentId = null;

            if (receptionCovidAirPortDto.RelationId == (int)RelationShipEnum.MySelf)
            {
                curPatient = await _patientRepository.Where(d => d.Person.Mobile == receptionCovidAirPortDto.Mobile)
                                                  .Include(d => d.PatientExtraInfo)
                                                  .Include(d => d.Person)
                                                  .FirstOrDefaultAsync();
            }
            else
            {
                //اگر شخص دیگیری بود و ثبت نام نشده بود باید برای اون اطلاعات پرسنلی ثبت کنیم
                //که فقط شماره تلفن ثبت شده و ایدی پرسن در داخل ریسپشن به عنوان پرنت ذخیره می شود

                curPatient = await _patientRepository.Where(d => d.Person.Mobile == receptionCovidAirPortDto.Mobile)
                                                  .Include(d => d.Person)
                                                  .FirstOrDefaultAsync();
                parentId = curPatient?.PersonId;

                if (curPatient == null)
                {
                    Patient patient = new Patient();
                    Person person = new Person();
                    person.Mobile = receptionCovidAirPortDto.Mobile;
                    patient.Person = person;

                    _patientRepository.Add(patient);
                    await _unitOfWork.SaveChangesAsync();
                    parentId = patient.PersonId;
                }

            }

            var map = ReceptionCovidAirPortMapper.Map(receptionCovidAirPortDto, curPatient);
            map.ParentId = parentId;
            map.SectionId = _workContext.SectionId;
            _receptionsRepository.Add(map);

            //map.ParentId = parentId;

            var maxReceptionId = (await MaxReceptionId()).Data;
            var maxIntenralId = (await MaxInternalId()).Data;
            long receptionId = (long)maxReceptionId;
            int intenralId = (int)maxIntenralId;

            map.ReceptionId = ++receptionId;

            if (_unitOfWork.Entry(map.Patient).State == EntityState.Added)
                map.Patient.InternalId = ++intenralId;

            map.ReceptionDate = DateTime.Now;

            var verifycode = Utilities.GenerateVerifyCode();

            var mapp = MobileActivationMapper.Map(new RequestVerifyDto { Mobile = receptionCovidAirPortDto.Mobile, VerifyCode = verifycode });

            _mobileActivationRepository.Add(mapp);

            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Databases.CommitTransaction();

            await SendSms(map, receptionCovidAirPortDto.Mobile, verifycode);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["ReceptionCovidForm.Response.AddSuccess"],//+
                Data = new { Code = receptionId, ReceptionId = map.Id, PersonId = map.Patient.Person.Id }
            };
        }

        public async Task<BaseResponseDto> UploadFile(BaseUploadFileDto<long> baseUploadFile)
        {
            var result = await _fileManagerService.Upload(new FileUploadDto { PrimeryKey = baseUploadFile.Id.ToString(), File = baseUploadFile.File, TableName = nameof(Person), FileGroupId = 1 });

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
            };
        }

        public async Task<BaseResponseDto> ResendSendSms(long receptionId)
        {

            var curRe = await _receptionsRepository.Where(d => d.Id == receptionId)
                                                    .Include(d => d.Patient.Person)
                                                    .Include(d => d.Parent)
                                                    .FirstOrDefaultAsync();

            var verifyCode = Utilities.GenerateVerifyCode();

            //اگر پدر بود باید به پدرش اسمس بشه اگر خودش بود به خود
            var mobile = "";

            if (curRe.RelationId == (int)RelationShipEnum.MySelf)
            {
                mobile = curRe.Patient.Person.Mobile;
            }
            else
            {
                mobile = curRe.Parent.Mobile;
            }

            var map = MobileActivationMapper.Map(new RequestVerifyDto { Mobile = mobile, VerifyCode = verifyCode });

            _mobileActivationRepository.Add(map);

            await _unitOfWork.SaveChangesAsync();

            await SendSms(curRe, curRe.Patient.Person.Mobile, verifyCode);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GlobalForm.Response.SendSms"]
            };
        }

        public async Task<bool> SendSms(Receptions receptions, string mobile, string verifycode)
        {
            await _smsService.SendSms(new SendSmsDto
            {
                Message = _sharedLocalizer["SendSms.Response.ReceptionVerifyCode"].Value
                    .Replace("{ReceptionCode}", receptions.SectionId + "-" + receptions.ReceptionId)
                    .Replace("{Code}", verifycode)
                    .Replace("{AnswerDate}", receptions.AnswerDate.ToDateStringForSlashTry())
                    .Replace("{FullName}", receptions.Patient?.Person?.FullNameThree)
                    .Replace("{newline}", "\r\n"),
                Mobile = mobile
            });

            return true;
        }

        public async Task<BaseResponseDto> ReceiptPrint(long receptionId)
        {
            DateTime? dt = DateTime.Now;

            var map = await _receptionsRepository.Where(d => d.Id == receptionId)
                .Select(g => new ReceiptDto
                {
                    AnswerDate = g.AnswerDate.ToDateStringTry(),
                    Code = (g.SectionId.ToString() ?? "") + "-" + g.ReceptionId.ToString(),
                    Date = dt.ToDateStringForSlashTry(),
                    FullName = g.Patient.Person != null ? g.Patient.Person.FullName : "",
                    Title = "Covid 19"
                })
                .FirstOrDefaultAsync();


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map
            };
        }


        public async Task<BaseResponseDto> VerifyCode(VerifyReceptionIdDto requestVerifyDto)
        {
            var curReception = await _receptionsRepository.Where(d => d.Id == requestVerifyDto.ReceptionId)
                                                        .Include(d => d.Patient.Person)
                                                        .Include(d => d.Parent)
                                                        .FirstOrDefaultAsync();

            //اگر پدر بود باید به پدرش اسمس بشه اگر خودش بود به خود
            var mobile = "";

            if (curReception.RelationId == (int)RelationShipEnum.MySelf)
            {
                mobile = curReception.Patient.Person.Mobile;
            }
            else
            {
                mobile = curReception.Parent.Mobile;
            }

            //check
            if (await _mobileActivationRepository.AnyAsync(d => d.Mobile == mobile && d.VerifyCode == requestVerifyDto.VerifyCode))
            {
                curReception.IsVerify = true;

                _receptionsRepository.Update(curReception);

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["GlobalForm.Response.UserNotFound"]}
                };
                var error = Utilities.CreateErrorMessage("NotFound", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Errors = error
                };
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["VerifyForm.Response.VerifySuccess"]
            };
        }

        public async Task<BaseResponseDto> Skip(ReceptionIdDto receptionIdDto)
        {
            var reception = await _receptionsRepository.Where(d => d.Id == receptionIdDto.ReceptionId).FirstOrDefaultAsync();
            reception.IsVerify = true;

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["VerifyForm.Response.VerifySuccess"]
            };
        }

        public async Task<ListResponseDto> SearchAirPort(FilterCovidAirPortDto filterCovidAirPortDto)
        {
            var query = _receptionsRepository
                        .OrderByDescending(d => d.Id)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(filterCovidAirPortDto.Code))
            {
                var arr = filterCovidAirPortDto.Code.Split('-');
                string receptionCode = string.Empty;
                string sectionId = string.Empty;
                if (arr.Length > 1)
                {
                    receptionCode = arr[1];
                    sectionId = arr[0];
                }
                else
                {
                    receptionCode = filterCovidAirPortDto.Code;
                }

                if (!string.IsNullOrEmpty(receptionCode))
                    query = query.Where(d => d.ReceptionId == receptionCode.TryToLong());

                if (!string.IsNullOrEmpty(sectionId))
                    query = query.Where(d => d.SectionId == sectionId.TryToInt());
            }

            if (!string.IsNullOrEmpty(filterCovidAirPortDto.Mobile))
            {
                query = query.Where(d => EF.Functions.Like(d.Patient.Person.Mobile, "%" + filterCovidAirPortDto.Mobile + "%") || EF.Functions.Like(d.Parent.Mobile, "%" + filterCovidAirPortDto.Mobile + "%"));
            }

            if (!string.IsNullOrEmpty(filterCovidAirPortDto.FullName))
            {
                query = query = query.Where(d => EF.Functions.Like(d.Patient.Person.FirstName + " " + d.Patient.Person.FatherName + " " + d.Patient.Person.GrandFatherName + " " + d.Patient.Person.LastName, "%" + filterCovidAirPortDto.FullName + "%"));

            }

            var lst = await query.ToPagedQuery(filterCovidAirPortDto).Select(ReceptionCovidAirPortMapper.MapList).AsNoTracking().ToListAsync();


            var lstFiles = await _filesService.GetFilesByFileGroupId(1, nameof(Person), lst.Select(d => d.PersonId.ToString()).ToList());

            lst.ForEach(d => d.FileId = lstFiles.Where(t => t.PrimeryKey == d.PersonId.ToString()).OrderByDescending(t => t.Id).Select(g => g.RefferKey).FirstOrDefault());

            return new ListResponseDto
            {
                Count = await query.CountAsync(),
                Data = lst,
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> AnswerPrint(long receptionId)
        {
            var cur = await _receptionsRepository.Where(d => d.Id == receptionId)
                                                    .Include(d => d.Parent)
                                                    .Include(d => d.Patient.Person)
                                                    .ThenInclude(d => d.Sex)
                                                    .Include(d => d.Section)
                                                    .FirstOrDefaultAsync();

            var map = ReceptionCovidAirPortMapper.AnswerMapper(cur);

            var lstFiles = await _filesService.GetFilesByFileGroupId(1, nameof(Person), cur.Patient.Person.Id.ToString());
            map.FileId = lstFiles.OrderByDescending(d => d.Id).OrderByDescending(d => d.Id).Select(g => g.RefferKey).FirstOrDefault();

            return new BaseResponseDto
            {
                Data = map,
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> EditCovidAirPort(ReceptionCovidAirPortDto receptionCovidAirPortDto, bool IsSendSms)
        {
            var resultValid = CheckValidate.Valid<ReceptionCovidAirPortDto>(new ReceptionCovidAirPortValidation(_sharedLocalizer), receptionCovidAirPortDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            var cur = await _receptionsRepository.Where(d => d.Id == receptionCovidAirPortDto.Id)
                .Include(d => d.Patient.Person)
                .FirstOrDefaultAsync();
            _unitOfWork.Databases.BeginTransaction();

            Patient curPatient = null;
            int? parentId = null;

            //if (receptionCovidAirPortDto.RelationId == (int)RelationShipEnum.MySelf)
            //{

            //    curPatient = await _patientRepository.Where(d => d.Person.Mobile == receptionCovidAirPortDto.Mobile)
            //                                      .Include(d => d.PatientExtraInfo)
            //                                      .Include(d => d.Person)
            //                                      .FirstOrDefaultAsync();
            //}
            //else
            //{
            //    //اگر شخص دیگیری بود و ثبت نام نشده بود باید برای اون اطلاعات پرسنلی ثبت کنیم
            //    //که فقط شماره تلفن ثبت شده و ایدی پرسن در داخل ریسپشن به عنوان پرنت ذخیره می شود

            //    curPatient = await _patientRepository.Where(d => d.Person.Mobile == receptionCovidAirPortDto.Mobile)
            //                                      .Include(d => d.Person)
            //                                      .FirstOrDefaultAsync();
            //    parentId = curPatient?.PersonId;

            //    if (curPatient == null)
            //    {
            //        Patient patient = new Patient();
            //        Person person = new Person();
            //        person.Mobile = receptionCovidAirPortDto.Mobile;
            //        patient.Person = person;

            //        _patientRepository.Add(patient);
            //        await _unitOfWork.SaveChangesAsync();
            //        parentId = patient.PersonId;
            //    }

            //}


            var map = ReceptionCovidAirPortMapper.Map(cur, receptionCovidAirPortDto, curPatient);
            ///map.ParentId = parentId;
            _receptionsRepository.Update(map);

            //map.ParentId = parentId;

            var verifycode = Utilities.GenerateVerifyCode();

            if (IsSendSms)
            {
                var mapp = MobileActivationMapper.Map(new RequestVerifyDto { Mobile = receptionCovidAirPortDto.Mobile, VerifyCode = verifycode });
                _mobileActivationRepository.Add(mapp);
            }

            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Databases.CommitTransaction();

            if (IsSendSms)
                await SendSms(map, receptionCovidAirPortDto.Mobile, verifycode);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["ReceptionCovidForm.Response.EditSuccess"],//+
                Data = new { Code = map.ReceptionId, ReceptionId = map.Id, PersonId = map.Patient.Person.Id }
            };
        }

        public async Task<BaseResponseDto> AnswerCovidSms(ReceptionIdDto receptionIdDto)
        {
            var curReception = await _receptionsRepository.Where(d => d.Id == receptionIdDto.ReceptionId)
                .Include(d => d.Patient.Person)
                .Include(d => d.Parent)
                .FirstOrDefaultAsync();

            //چک میکنیم پذیرش به  اسم خودش هست یا ولی اگر ولی بود به او اسمس میکنیم
            string mobile = string.Empty;
            if (curReception.RelationId == (int)RelationShipEnum.MySelf)
            {
                mobile = curReception.Patient.Person.Mobile;
            }
            else
            {
                mobile = curReception.Parent.Mobile;
            }

            var base64 = Utilities.Base64Encode(receptionIdDto.ReceptionId.ToString());

            var link = _configuration["AnswerCovidUrl"].Replace("{receptionId}", Uri.EscapeDataString(base64));

            await _smsService.SendSms(new SendSmsDto { Mobile = mobile, Message = _sharedLocalizer["ReceptionCovidForm.Response.AnswerNotif"].Value.Replace("{LinkAnswer}", link).Replace("{newline}", "\r\n").Replace("{FullName}", curReception.Patient.Person.FullName) });

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GlobalForm.Response.SendSms"]
            };
        }

        public async Task<BaseResponseDto> CheckByMobile(RequestMobileVerifyDto requestMobileVerifyDto)
        {
            var curPerson = await _personRepository.Where(d => d.Mobile == requestMobileVerifyDto.Mobile).FirstOrDefaultAsync();

            var map = PersonMapper.Map(curPerson);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map
            };
        }

        public async Task<BaseResponseDto> AddLab(ReceptionLabDto receptionCovidDto)
        {
            var resultValid = CheckValidate.Valid(new ReceptionLabValidation(_sharedLocalizer), receptionCovidDto);
            if (resultValid.Status == ResponseStatus.NotValid) return resultValid;

            var service = _serviceRepository.Where(d => d.LocalCode == "94756-4").FirstOrDefault();
            var map = ReceptionMapper.Map(receptionCovidDto, _workContext.UserId.GetValueOrDefault(), _workContext.SectionId.GetValueOrDefault(), service.Id);
            var maxReceptionId = (await MaxReceptionId()).Data;
            var maxIntenralId = (await MaxInternalId()).Data;
            long receptionId = (long)maxReceptionId;
            int intenralId = (int)maxIntenralId;

            map.ReceptionId = ++receptionId;
            map.Patient.InternalId = ++intenralId;
            _receptionsRepository.Add(map);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = new { ReceptionCode = map.ReceptionId },
                Message = _sharedLocalizer["ReceptionForm.Response.Add"]
            };
        }

        public async Task<BaseResponseDto> EditLab(ReceptionLabDto receptionCovidDto)
        {
            var cur = await _receptionsRepository.Where(d => d.Id == receptionCovidDto.Id)
                .Include(d => d.Patient)
                .ThenInclude(d => d.PatientExtraInfo)
                .Include(d => d.Patient.Person)
                .FirstOrDefaultAsync();

            var map = ReceptionMapper.Map(cur, receptionCovidDto, 0, 0, 0);

            _receptionsRepository.Update(map);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = new { ReceptionCode = map.ReceptionId },
                Message = _sharedLocalizer["ReceptionForm.Response.Edit"]
            };
        }

        public async Task<ListResponseDto> SearchLab(FilterReceptionLabDto filterReceptionLabDto)
        {
            var sectionId = _workContext.SectionId;
            var parameters = new Dictionary<string, string>();

            var query = _receptionsRepository.OrderByDescending(d => d.Id).AsQueryable();

            query = query.Where(d => d.SectionId == _workContext.SectionId);

            if (!string.IsNullOrEmpty(filterReceptionLabDto.ReceptionFromDate))
            {
                parameters.Add("@receptionFromDate", filterReceptionLabDto.ReceptionFromDate);
                query = query.Where(d => d.ReceptionDate.Value.Date >= filterReceptionLabDto.ReceptionFromDate.TryToDateTime().Value.Date);
            }
            
            if (!string.IsNullOrEmpty(filterReceptionLabDto.ReceptionToDate))
            {
                parameters.Add("@receptionToDate", filterReceptionLabDto.ReceptionToDate);
                query = query.Where(d => d.ReceptionDate.Value.Date <= filterReceptionLabDto.ReceptionToDate.TryToDateTime().Value.Date);
            }


            if (!string.IsNullOrEmpty(filterReceptionLabDto.FullName))
            {
                parameters.Add("@fullName", filterReceptionLabDto.FullName);
                query = query.Where(d => EF.Functions.Like((!string.IsNullOrEmpty(d.Patient.Person.FirstName) ? d.Patient.Person.FirstName.Trim() : "") + " " + (!string.IsNullOrEmpty(d.Patient.Person.FatherName) ? d.Patient.Person.FatherName.Trim() : "") + " " + (!string.IsNullOrEmpty(d.Patient.Person.GrandFatherName) ? d.Patient.Person.GrandFatherName.Trim() : "") + " " + (!string.IsNullOrEmpty(d.Patient.Person.LastName) ? d.Patient.Person.LastName.Trim() : ""), $"%{filterReceptionLabDto.FullName}%"));
            }


            if (filterReceptionLabDto.ReceptionCode != null)
            {
                parameters.Add("@receptionCode", filterReceptionLabDto.ReceptionCode.ToString());
                query = query.Where(d => d.ReceptionId == filterReceptionLabDto.ReceptionCode);
            }

            if (filterReceptionLabDto.SexId != null)
            {
                query = query.Where(d => d.Patient.Person.SexId == filterReceptionLabDto.SexId);
            }

            if (!string.IsNullOrEmpty(filterReceptionLabDto.FileNo))
            {
                query = query.Where(d => d.Patient.FileNo.Contains(filterReceptionLabDto.FileNo));
            }


            if (!string.IsNullOrEmpty(filterReceptionLabDto.LatinName))
            {
                parameters.Add("@latinName", filterReceptionLabDto.LatinName);
                query = query.Where(d => EF.Functions.Like(d.Patient.Person.LatinName, $"%{filterReceptionLabDto.LatinName}%"));
            }


            if (!string.IsNullOrEmpty(filterReceptionLabDto.Mobile))
            {
                parameters.Add("@mobile", filterReceptionLabDto.Mobile);
                query = query.Where(d => EF.Functions.Like(d.Patient.Person.Mobile, $"%{filterReceptionLabDto.Mobile}%"));
            }


            if (!string.IsNullOrEmpty(filterReceptionLabDto.PassportNumber))
            {
                parameters.Add("@passportNumber", filterReceptionLabDto.PassportNumber);
                query = query.Where(d => d.Patient.PatientExtraInfo.Any(t => EF.Functions.Like(t.PassportNumber, $"%{filterReceptionLabDto.PassportNumber}%")));
            }


            if (filterReceptionLabDto.NationalityIds?.Count > 0)
            {
                parameters.Add("@nationalityId",string.Join(",",filterReceptionLabDto.NationalityIds));
                query = query.Where(d => d.Patient.PatientExtraInfo.Any(t => filterReceptionLabDto.NationalityIds.Contains(t.NationalityId.GetValueOrDefault())));
            }


            if (filterReceptionLabDto.RegisterUserIds?.Count > 0)
            {
                parameters.Add("@registerUserId", string.Join(",", filterReceptionLabDto.RegisterUserIds));
                query = query.Where(d => d.ReceptionHistory.Any(t => filterReceptionLabDto.RegisterUserIds.Contains(t.UserId.GetValueOrDefault())));
            }

            if (filterReceptionLabDto.AnswerUserIds?.Count > 0)
            {
                parameters.Add("@answerUserId", string.Join(",", filterReceptionLabDto.AnswerUserIds));
                query = query.Where(d => d.ReceptionService.Any(t => filterReceptionLabDto.AnswerUserIds.Contains(t.AnswerUserId.GetValueOrDefault())));
            }


            if (!string.IsNullOrEmpty(filterReceptionLabDto.Result))
            {
                parameters.Add("@result", filterReceptionLabDto.Result);
                query = query.Where(d => d.ReceptionService.Any(t => t.Answer1.Any(r => r.Result.Contains(filterReceptionLabDto.Result))));
            }

            if (!string.IsNullOrEmpty(filterReceptionLabDto.Title))
            {
                query = query.Where(d => EF.Functions.Like(d.Note, $"%{filterReceptionLabDto.Title}%"));
            }

            if (filterReceptionLabDto.StatusId != null)
            {
                query = query.Where(d => d.ReceptionService.Any(r => r.StatusId == filterReceptionLabDto.StatusId));
            }

            if (filterReceptionLabDto.HasValidPaging())
            {
                parameters.Add("@pageSize", filterReceptionLabDto.PageSize.ToString());
                parameters.Add("@pageNumber", filterReceptionLabDto.PageNumber.ToString());
            }

            var list = await _unitOfWork.Set<SearchReceptionSp>()
                .FromSqlInterpolated($@"[dbo].[SearchReception] 
                    @sectionId={sectionId},
                    @answerUserId={parameters.GetString("@answerUserId")},
                    @registerUserId={parameters.GetString("@registerUserId")},
                    @nationalityId={parameters.GetString("@nationalityId")},
                    @passportNumber={parameters.GetString("@passportNumber")},
                    @mobile={parameters.GetString("@mobile")},
                    @latinName={parameters.GetString("@latinName")},
                    @receptionCode={parameters.GetString("@receptionCode")},
                    @fullName={parameters.GetString("@fullName")},
                    @receptionFromDate={parameters.GetDateTime("@receptionFromDate")},
                    @receptionToDate={parameters.GetDateTime("@receptionToDate")},
                    @result={parameters.GetString("@result")},
                    @pageSize={parameters.GetInt("@pageSize",10)},
                    @pageNumber={parameters.GetInt("@pageNumber",1)}")
                .ToListAsync();

            return new ListResponseDto
            {
                //Data =await query.ToPagedQuery(filterReceptionLabDto).Select(ReceptionMapper.MapList).ToListAsync(),
                Data = list.Select(ReceptionMapper.Map).ToList(),
                Status = ResponseStatus.Success,
                Count = list.Select(x=>x.CountAll).FirstOrDefault()
            };
        }

        public async Task<BaseResponseDto> SetLabAnswer(AnswerLabDto answerLabDto)
        {
            var cur = await _answerRepository.Where(d => d.ReceptionServiceId == answerLabDto.ReceptionServiceId)
                                           .FirstOrDefaultAsync();

            var curReceptionService = await _receptionServiceRepository.Where(d => d.Id == answerLabDto.ReceptionServiceId).Include(r => r.Reception).FirstOrDefaultAsync();

            if (cur == null)
            {
                _answerRepository.Add(new Answer1 { Result = answerLabDto.Result, ReceptionServiceId = answerLabDto.ReceptionServiceId, Comment = answerLabDto.Note });
            }
            else
            {
                cur.Result = answerLabDto.Result;
                cur.Comment = answerLabDto.Note;
                _answerRepository.Update(cur);
            }

            //set status 

            curReceptionService.Reception.AnswerDate = answerLabDto.AnswerDate.TryToDateTime();
            curReceptionService.StatusId = (int)ReceptionServiceStatusEnum.Answered;
            //curReceptionService.StatusId = (int)ReceptionServiceStatusEnum.Answered;
            curReceptionService.AnswerUserId = _workContext.UserId;

            _receptionServiceRepository.Update(curReceptionService);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["SetAnswerCovid.Response.SetAnswerSuccess"]
            };
        }

        public async Task<BaseResponseDto> LabCovidReport(long receptionId)
        {
            var data = await _receptionsRepository.Where(x => x.Id == receptionId)
                .Select(ReceptionMapper.MapLabReport).ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = data
            };
        }

        public async Task<BaseResponseDto> GetLabById(long id)
        {
            var cur = await _receptionsRepository
                .Include(d => d.Patient)
                .ThenInclude(d => d.PatientExtraInfo)
                .ThenInclude(d => d.Nationality)
                .Include(d => d.Patient.Person)
                .Include(d => d.Patient.Person.Sex)
                .Where(d => d.Id == id)
                .Select(g => ReceptionMapper.Map(g))
                .FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = cur
            };
        }

        #endregion


    }
}
