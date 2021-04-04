using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Context;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Application.Mapper;
using Portal.DAL.Extensions;
using Portal.DTO.Doctor;
using Portal.DTO.PatientCard;
using Portal.Infrastructure.Mapper;
using Portal.Interface;
using static Portal.Infrastructure.Utilities;

namespace Portal.Application.Service
{
    public class PatientCardService : IPatientCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Prescription> _prescriptionRepository;
        private readonly DbSet<PrescriptionDetailService> _prescriptionDetailServiceRepository;
        private readonly DbSet<PrescriptionDetailDrug> _prescriptionDetailDrugRepository;
        private readonly DbSet<PrescriptionServiceResult> _prescriptionServiceResultRepository;
        private readonly DbSet<PrescriptionShare> _prescriptoinShareRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IWorkContextService _workContextService;

        public PatientCardService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer, IWorkContextService workContextService)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _prescriptionRepository = _unitOfWork.Set<Prescription>();
            _prescriptionDetailServiceRepository = _unitOfWork.Set<PrescriptionDetailService>();
            _prescriptionDetailDrugRepository = _unitOfWork.Set<PrescriptionDetailDrug>();
            _prescriptionServiceResultRepository = _unitOfWork.Set<PrescriptionServiceResult>();
            _prescriptoinShareRepository = _unitOfWork.Set<PrescriptionShare>();
            _sharedLocalizer = sharedLocalizer;
            _workContextService = workContextService;
        }

        public async Task<ListResponseDto> ListPrescription(RequestPatientCardDto dto, bool isCurrentUser = false)
        {
            var queryable = _prescriptionRepository
                .If(!string.IsNullOrEmpty(dto.PrescriptionFromDate),x=>x.Where(y => y.CreateDate!=null && y.CreateDate.Value.Date >= dto.PrescriptionFromDate.TryToDateTime()))
                .If(!string.IsNullOrEmpty(dto.PrescriptionToDate),x=>x.Where(y => y.CreateDate!=null && y.CreateDate.Value.Date <= dto.PrescriptionToDate.TryToDateTime()))
                .If(isCurrentUser==true,x=>x.Where(y => y.Reception.Patient.Person.Users.Any(z => z.Id == _workContextService.UserId)))
                
                // تنها تجویزهایی نشان داده میشود که دارو داشته باشند
                .If(isCurrentUser==true,x=>x.Where(y => y.PrescriptionDetailDrug.Any()))
                .OrderByDescending(x => x.Id).AsQueryable();

            var count = await queryable.CountAsync();
            if(count==0)return ListResponseDto.Success();

            var prescriptions = await queryable.ToPagedQuery(dto)
                .Select(Mapper.PatientCardMapper.MapPrescriptions)
                .AsNoTracking().ToListAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = prescriptions
            };
        }

        public async Task<BaseResponseDto> ListTest(BaseRequestPost<long> baseRequest)
        {
            var lst = await _prescriptionDetailServiceRepository
                                .Where(d => d.PrescriptionId == baseRequest.Id && d.Service.Parent.LocalCode == GroupServiceCode.Labratuary)
                                .Include(d => d.Service)
                                // .ToPagedQuery(baseRequest)
                                .ToListAsync();

            var tests = string.Join(",", lst.Select(g => g.Service.Title).ToList());

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = tests,
            };
        }

        public async Task<BaseResponseDto> ListDrug(BaseRequestPost<long> baseRequest)
        {
            var lst = await _prescriptionDetailDrugRepository
                                .Where(d => d.PrescriptionId == baseRequest.Id)
                                //.Include(d => d.Drug)
                                .Select(g => Mapper.PatientCardMapper.MapDrug(g))
                                //  .ToPagedQuery(baseRequest)
                                .ToListAsync();

            //var tests = string.Join(",", lst.Select(g => g.Service.Title).ToList());

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst,
            };
        }

        public async Task<BaseResponseDto> VitalSign(BaseRequestPost<long> baseRequest)
        {
            var lst = await _prescriptionServiceResultRepository
                                  .Where(d => d.PrescriptionId == baseRequest.Id)
                                    //.Select(g => new PatientCardListVitalSignDto
                                    //{
                                    //    Id = g.Id,
                                    //    BMI = g.Bmi.ToString(),
                                    //    BP = g.Bpmean.ToString(),
                                    //    BR = g.Breathing.ToString(),
                                    //    //eGFR=g.
                                    //    Height = g.Height.GetValueOrDefault(),
                                    //    Weight = g.Weight.GetValueOrDefault(),
                                    //    Creatinine = g.Creatinine.ToString(),
                                    //    PrescriptionDate = g.CreateDate.ToDateStringTry(),
                                    //    SPO2 = g.Spo2.ToString(),
                                    //    SkinRace = g.SkinRace.ToString()
                                    //})
                                    .Where(d => d.Service.InterNationalCode == "431314004" || d.Service.InterNationalCode == "276885007" || d.Service.InterNationalCode == "271650006" || d.Service.InterNationalCode == "60621009" || d.Service.InterNationalCode == "78564009" || d.Service.InterNationalCode == "27113001" || d.Service.InterNationalCode == "50373000" || d.Service.InterNationalCode == "737105002" || d.Service.InterNationalCode == "103579009" || d.Service.InterNationalCode == "271649006" || d.Service.InterNationalCode == "251075007" || d.Service.InterNationalCode == "431314004")

                                  .Select(g => new ResponsePrescriptionServiceResultReport
                                  {
                                      Id = g.Id,
                                      Code = g.Service.Code.ToString(),
                                      Result = g.Result,
                                      Result2 = g.Result2,
                                      Title = g.Service.Title

                                  })
                                  .ToListAsync();
            //var tests = string.Join(",", lst.Select(g => g.Service.Title).ToList());

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst,
            };
        }

        public async Task<BaseResponseDto> Radilogy(BaseRequestPost<long> baseRequest)
        {
            var lst = await _prescriptionDetailServiceRepository
                                .Where(d => d.PrescriptionId == baseRequest.Id)
                                  //.Select(g => new PatientCardListVitalSignDto
                                  //{
                                  //    Id = g.Id,
                                  //    BMI = g.Bmi.ToString(),
                                  //    BP = g.Bpmean.ToString(),
                                  //    BR = g.Breathing.ToString(),
                                  //    //eGFR=g.
                                  //    Height = g.Height.GetValueOrDefault(),
                                  //    Weight = g.Weight.GetValueOrDefault(),
                                  //    Creatinine = g.Creatinine.ToString(),
                                  //    PrescriptionDate = g.CreateDate.ToDateStringTry(),
                                  //    SPO2 = g.Spo2.ToString(),
                                  //    SkinRace = g.SkinRace.ToString()
                                  //})// فعلا چون ایدی داخل سرویس ندایرم این ایدی قرار میدهیم تا بتوانی سرویس رادیلوزی یه پذیرش تفکیک کنیم
                                  .Where(d => d.Service.LocalCode == GroupServiceCode.Radilogy)

                                .Select(g => new ResponseNote
                                {
                                    Note = g.Note
                                    //  Title = g.Service.Title

                                })
                                .ToListAsync();

            //var tests = string.Join(",", lst.Select(g => g.Service.Title).ToList());

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst,
            };
        }

        public async Task<BaseResponseDto> PatientHistory(BaseRequestPost<long> baseRequest)
        {
            var lst = await _prescriptionServiceResultRepository
                                .Where(d => d.PrescriptionId == baseRequest.Id)
                                //.Select(g => new PatientCardListVitalSignDto
                                //{
                                //    Id = g.Id,
                                //    BMI = g.Bmi.ToString(),
                                //    BP = g.Bpmean.ToString(),
                                //    BR = g.Breathing.ToString(),
                                //    //eGFR=g.
                                //    Height = g.Height.GetValueOrDefault(),
                                //    Weight = g.Weight.GetValueOrDefault(),
                                //    Creatinine = g.Creatinine.ToString(),
                                //    PrescriptionDate = g.CreateDate.ToDateStringTry(),
                                //    SPO2 = g.Spo2.ToString(),
                                //    SkinRace = g.SkinRace.ToString()
                                //})
                                .Where(d => d.Service.InterNationalCode == "33962009" || d.Service.InterNationalCode == "422625006" || d.Service.InterNationalCode == "417662000" || d.Service.InterNationalCode == "161615003" || d.Service.InterNationalCode == "258232002" || d.Service.InterNationalCode == "371441004" || d.Service.InterNationalCode == "78564009" || d.Service.InterNationalCode == "86290005" || d.Service.LocalCode == GroupServiceCode.MedicalHistory)
                                .Select(g => new ResponsePrescriptionServiceResultReport
                                {
                                    Id = g.Id,
                                    Code = g.Service.Code.ToString(),
                                    Result = g.Result,
                                    Result2 = g.Result2,
                                    Title = g.Service.Title

                                })
                                .ToListAsync();
            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst,
            };
        }

        public async Task<BaseResponseDto> PrescriptionInfo(BaseRequestPost<long> baseRequest)
        {
            var prescription = await _prescriptionRepository.Where(d => d.Id == baseRequest.Id)
                .Include(x => x.Doctor.Person)
                .Include(x => x.Reception.Patient.Person)
                .Include(x => x.Doctor.DoctorCertificate).ThenInclude(x=>x.Certificate)
                .FirstOrDefaultAsync();

            var prescriptionInfoDto = new PrescriptionInfoDto
            {
                DoctorNote = prescription?.MainDoctorNote,
                PatientFullName = prescription?.Reception?.Patient?.Person?.FullName,
                Drugs = await ListDrug(baseRequest),
                Tests = (await ListTest(baseRequest)).Data,
                VitalSign = await VitalSign(baseRequest),
                Radilogies = await Radilogy(baseRequest),
                PatientHistory = await PatientHistory(baseRequest),
                DoctorInfoDto = new DoctorInfoDto
                {
                    Id = prescription?.Doctor?.Person?.Id ?? 0,
                    FullName = prescription?.Doctor?.Person?.LatinName,
                    ArabicFullName = prescription?.Doctor?.Person?.LastName,
                    Note = prescription?.Doctor?.Note,
                    // TODO باید مپر شود
                    Certificates = prescription?.Doctor?.DoctorCertificate?.Select(x=>new CertificateDto
                    {
                        Id = x.CertificateId.GetValueOrDefault(),
                        Title = x.Certificate?.Title,
                        TitleLang2 = x.Certificate?.TitleLang2
                    }).ToList()
                }
            };

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = prescriptionInfoDto
            };
        }

        public async Task<BaseResponseDto> PrescriptionByShareId(string shareId)
        {
            var prescriptionId = (await GetShare(shareId)).Data.TryToLong();
            if (prescriptionId == 0) return BaseResponseDto.Success("");

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = (await PrescriptionInfo(new BaseRequestPost<long>{Id = prescriptionId})).Data
            };
        }

        public async Task<BaseResponseDto> SetShare(long id)
        {
            var cur = await _prescriptoinShareRepository.Where(d => d.PrescriptoinId == id).FirstOrDefaultAsync();

            var key = string.Empty;

            if (cur == null)
            {
                key = Guid.NewGuid().ToString();
                _prescriptoinShareRepository.Add(new PrescriptionShare
                {
                    FileName = key,
                    PrescriptoinId = id,
                    CreateDate = DateTime.Now
                });
            }
            else key = cur.FileName;

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["PrescriptionForm.Response.ShareSuccess"],
                Data = key
            };
        }

        public async Task<BaseResponseDto> GetShare(string fileName)
        {
            var result = await _prescriptoinShareRepository
                .Where(x => x.FileName == fileName).FirstOrDefaultAsync();
            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = result?.PrescriptoinId
            };
        }

        public async Task<ListResponseDto> ListCurrentRadilogy()
        {
            var lst = await _prescriptionDetailServiceRepository.Where(d => d.Prescription.Reception.Patient.Person.Users.Any(t => t.Id == _workContextService.UserId) && d.Service.LocalCode == GroupServiceCode.Radilogy)
                                .Select(g => new ResponseNote
                                {
                                    Id = g.Id,
                                    Note = g.Note,
                                    PrescriptionId = g.Prescription != null ? g.Prescription.Id : 0,
                                    PrescriptionDate = g.Prescription != null ? g.Prescription.PrescriptionDate : null,
                                    PatientFullName = g.Prescription.Reception.Patient.Person != null ? g.Prescription.Reception.Patient.Person.FullName : "",
                                    DoctorInfo = new DoctorInfoDto
                                    {
                                        Id = g.Prescription.DoctorId.GetValueOrDefault(),
                                        FullName = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.LatinName : "",
                                        ArabicFullName = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.LastName : "",
                                        Address = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.Address : "",
                                        Phone = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.Phone : "",
                                        Email = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.Email : "",
                                        Note = g.Prescription.Doctor.Note,
                                        ExpertiseTitle = g.Prescription.Doctor.Expertise != null ? g.Prescription.Doctor.Expertise.Title : "",
                                        Degrees = g.Prescription.Doctor.DoctorDegree != null && g.Prescription.Doctor.DoctorDegree.Count > 0 ? g.Prescription.Doctor.DoctorDegree.Select(y => new DoctorDegreeDto
                                        {
                                            Id = y.Id,
                                            DoctorId = y.DoctorId,
                                            DoctorFulName = y.Doctor.Person != null ? y.Doctor.Person.FullName : "",
                                            DegreeId = y.DegreeId,
                                            DegreeTitle = y.Degree != null ? y.Degree.Title : "",
                                            DateOfIssue = y.DateOfIssue
                                        }).ToList() : null,
                                        WorkTimes = g.Prescription.Doctor.Center != null ? (g.Prescription.Doctor.Center.CenterWorkItem != null ? g.Prescription.Doctor.Center.CenterWorkItem.Select(t => new ListMultiResponse<int> { Id = t.Id, Title = t.WorkItem.Title }).ToList() : null) : null
                                    }
                                })
                                .ToListAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst
            };
        }

        public async Task<ListResponseDto> CurrentRadilogyById(long id)
        {
            var lst = await _prescriptionDetailServiceRepository
                .Where(d => d.Prescription.Reception.Patient.Person.Users.Any(t => t.Id == _workContextService.UserId) && d.Service.LocalCode == GroupServiceCode.Radilogy)
                //.Where(d => d.Service.LocalCode == GroupServiceCode.Radilogy)
                .Where(x => x.Id == id)
                .Select(g => new ResponseNote
                {
                    Id = g.Id,
                    Note = g.Note,
                    PrescriptionId = g.Prescription != null ? g.Prescription.Id : 0,
                    PrescriptionDate = g.Prescription != null ? g.Prescription.PrescriptionDate : null,
                    PatientFullName = g.Prescription.Reception.Patient.Person != null ? g.Prescription.Reception.Patient.Person.FullName : "",
                    DoctorInfo = new DoctorInfoDto
                    {
                        Id = g.Prescription.DoctorId.GetValueOrDefault(),
                        FullName = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.LatinName : "",
                        ArabicFullName = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.LastName : "",
                        Address = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.Address : "",
                        Phone = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.Phone : "",
                        Email = g.Prescription.Doctor.Person != null ? g.Prescription.Doctor.Person.Email : "",
                        Note = g.Prescription.Doctor.Note,
                        ExpertiseTitle = g.Prescription.Doctor.Expertise != null ? g.Prescription.Doctor.Expertise.Title : "",
                        Degrees = g.Prescription.Doctor.DoctorDegree != null && g.Prescription.Doctor.DoctorDegree.Count > 0 ? g.Prescription.Doctor.DoctorDegree.Select(y => new DoctorDegreeDto
                        {
                            Id = y.Id,
                            DoctorId = y.DoctorId,
                            DoctorFulName = y.Doctor.Person != null ? y.Doctor.Person.FullName : "",
                            DegreeId = y.DegreeId,
                            DegreeTitle = y.Degree != null ? y.Degree.Title : "",
                            DateOfIssue = y.DateOfIssue
                        }).ToList() : null,
                        WorkTimes = g.Prescription.Doctor.Center != null ? (g.Prescription.Doctor.Center.CenterWorkItem != null ? g.Prescription.Doctor.Center.CenterWorkItem.Select(t => new ListMultiResponse<int> { Id = t.Id, Title = t.WorkItem.Title }).ToList() : null) : null,
                        Certificates = g.Prescription.Doctor.DoctorCertificate!=null? g.Prescription.Doctor.DoctorCertificate.Select(x => new CertificateDto
                        {
                            Id = x.CertificateId.GetValueOrDefault(),
                            Title = x.Certificate!=null? x.Certificate.Title:"",
                            TitleLang2 = x.Certificate != null ? x.Certificate.TitleLang2:""
                        }).ToList():null
                    }
                }).FirstOrDefaultAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst
            };
        }

        public async Task<BaseResponseDto> RadiologyByShareId(string shareId)
        {
            var prescriptionId = (await GetShare(shareId)).Data.TryToLong();

            if (prescriptionId == 0) return BaseResponseDto.Success("");

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = (await CurrentRadilogyById(prescriptionId)).Data
            };
        }
    }
}
