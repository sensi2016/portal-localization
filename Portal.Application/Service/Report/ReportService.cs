using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using His.Reception.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Portal.Application.Interface;
using Portal.Application.Mapper;
using Portal.Context;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.DTO.PatientCard;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Infrastructure.Mapper;
using Portal.Interface;
using static Portal.DTO.ConstProperty;

// ReSharper disable once CheckNamespace
namespace Portal.Application.Service
{
    public class ReportService : IReportService
    {
        private readonly DbSet<PrescriptionShare> _prescriptoinShareRepository;
        private readonly DbSet<AnswerService> _answerServiceRepository;
        private readonly DbSet<Receptions> _receptionRepository;
        private readonly DbSet<RefferFrom> _refferFromRepository;
        private readonly DbSet<GeneralStatus> _generalStatusRepository;
        private readonly IPatientCardService _patientCardService;
        private readonly IPortalResourceService _portalResourceService;

        public ReportService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer, IPortalResourceService hisResourceService,
            IWorkContextService workContext, ISettingService settingService, IPatientCardService patientCardService, IPortalResourceService portalResourceService
            )
        {
            _prescriptoinShareRepository = unitOfWork.Set<PrescriptionShare>();
            _answerServiceRepository = unitOfWork.Set<AnswerService>();
            _receptionRepository = unitOfWork.Set<Receptions>();
            _refferFromRepository = unitOfWork.Set<RefferFrom>();
            _generalStatusRepository = unitOfWork.Set<GeneralStatus>();
            _patientCardService = patientCardService;
            _portalResourceService = portalResourceService;
        }

        public async Task<ListResponseDto> ReportDate(ReportDto report)
        {
            var result = new ListResponseDto { Status = ResponseStatus.NotFound };

            if (report.ReportName.ToLower() == "PrescriptionShare".ToLower())
            {
                var parameters = Utilities.MapTowObject<PrescriptionShareReportParametersDto>(report.Parameters);
                //parameters.IsPrint = true;
                result = (await PrescriptionShare(parameters)).ToListResponseDto();
            }

            return result;
        }

        public async Task<BaseResponseDto> ReportHeader(string reportTitle)
        {
            //var sectionId = _workContext.SectionId;
            //var sectionTitle = await _sectionRepository.Where(x => x.Id == sectionId).Select(x => x.Title)
            //    .FirstOrDefaultAsync();

            //var allSettings = new List<SettingItemDto>();
            //var settingRequest = await _settingService.GetAll();
            //if (settingRequest.Data != null) allSettings = (List<SettingItemDto>)settingRequest.Data;

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                //Data = new ReportHeaderDto
                //{
                //    HospitalName = allSettings.Where(x => x.Name == "HospitalName").FirstOrDefault()?.Value,
                //    Title = reportTitle,
                //    DepartmentTitle = sectionTitle,
                //    Description = ""
                //}
            };
        }

        public Task<BaseResponseDto> ReportFooter(string reportTitle)
        {
            return null;
        }

        public async Task<BaseResponseDto> PatientInfo(long receptionId)
        {
            //var patientInfo = await _receptionRepository
            //    .Where(x => x.Id == receptionId)
            //    .Include(x => x.Patient.Person.Sex)
            //    .Include(x => x.BedReception).ThenInclude(x => x.Bed.Room)
            //    .Include(x => x.HospitalizationDoctor.Person)
            //    .Include(x => x.Section)
            //    //.Select(x => ReportMapper.Map(x))
            //    .FirstOrDefaultAsync();

            //// age calc
            //if (patientInfo.BirthDate.IsValidDateTime())
            //{
            //    var age = DateTime.Today.Year - patientInfo.BirthDate.GetValueOrDefault().Year;
            //    patientInfo.Age = age;
            //}

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                // Data = patientInfo
            };
        }

        public async Task<BaseResponseDto> PrescriptionShare(PrescriptionShareReportParametersDto prescriptionShareReportParametersDto)
        {
            var cur = await _prescriptoinShareRepository.Where(d => d.FileName == prescriptionShareReportParametersDto.Key)
                .Include(d => d.Prescriptoin.Reception.Patient.Person)
                .ThenInclude(d => d.Sex)
                .Include(d => d.Prescriptoin.Doctor.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            var patientcard = (PrescriptionInfoDto)(await _patientCardService.PrescriptionInfo(new BaseRequestPost<long> { Id = cur.PrescriptoinId.GetValueOrDefault() })).Data;


            object translate = null;

            translate = (await _portalResourceService.GetLabelReport("PrescriptionShareReport", (new List<Type> {

                typeof(PrescriptionReportInfoDto),
                typeof(PrescriptionReportInfoInfoDto),
                typeof(DrugDto),
                typeof(LaboratoryAnswerDto),
                typeof(RadiologReportDto),
                typeof(PatientInfoDto),
                typeof(VitalSignDto),


            }))).Data;

            var report = new PrescriptionReportInfoDto();

            // report.Drugs = new List<DrugDto>();
            // //report.Drugs.Add(new DrugDto());
            // report.Radiologies = new List<RadiologyDto>();
            //// report.Radiologies.Add(new RadiologyDto { });
            // report.VitalSigns = new VitalSignDto();
            // report.PatientHistories = new PatientInfoDto();
            // report.LaboratoryAnswers = new List<LaboratoryAnswerDto>();
            //report.LaboratoryAnswers.Add(new LaboratoryAnswerDto());

            report.PatientInfo = new PrescriptionReportInfoInfoDto();

            report.PatientInfo.FullName = cur?.Prescriptoin?.Reception?.Patient?.Person?.FullName;
            report.PatientInfo.NationalCode = cur?.Prescriptoin?.Reception?.Patient?.Person?.NationalCode;
            report.PatientInfo.Phone = cur?.Prescriptoin?.Reception?.Patient?.Person?.Phone;
            report.PatientInfo.FatherName = cur?.Prescriptoin?.Reception?.Patient?.Person?.FatherName;
                 
            report.PatientInfo.GrandFatherName = cur?.Prescriptoin?.Reception?.Patient?.Person?.GrandFatherName;
            report.PatientInfo.SexAndAge = cur?.Prescriptoin?.Reception?.Patient?.Person?.Sex?.Title + " - " + cur?.Prescriptoin?.Reception?.Patient?.Person?.Age;
                  
            report.PatientInfo.DoctorName = cur?.Prescriptoin?.Doctor?.Person?.FullName;
            report.PatientInfo.ClinicName = cur?.Prescriptoin?.Doctor?.Person?.FullName;
            report.PatientInfo.ClinicPhone = cur?.Prescriptoin?.Doctor?.Person?.FullName;
           

            //report.Tests = patientcard.Tests.ToString();
            report.Radiologies = (((List<ResponseNote>)(((BaseResponseDto)patientcard.Radilogies).Data)).Select(g => new RadiologyDto { Name = JsonConvert.DeserializeObject<RadiologyDto>(g.Note).Name, Note = JsonConvert.DeserializeObject<RadiologyDto>(g.Note).Note }).ToList()) ?? new List<RadiologyDto>();
            report.Drugs = (((List<DrugWinAppBackendDto>)(((BaseResponseDto)patientcard.Drugs).Data)).Select(g => new DrugDto { DrugName = g.DrugName, DrugDetail = g.Details, DrugNote = g.Notes, DurationTime = g.Meal }).ToList()) ?? new List<DrugDto>();            var vital = ((((List<ResponsePrescriptionServiceResultReport>)(((BaseResponseDto)patientcard.VitalSign).Data)).Select(g => new ResponsePrescriptionServiceResultReport { Code = g.Code, Id = g.Id, Result = g.Result, Result2 = g.Result2, Title = g.Title }).ToList())) ?? new List<ResponsePrescriptionServiceResultReport>();
            var patientHistory = (((List<ResponsePrescriptionServiceResultReport>)(((BaseResponseDto)patientcard.PatientHistory).Data)).Select(g => new ResponsePrescriptionServiceResultReport { Code = g.Code, Id = g.Id, Result = g.Result, Result2 = g.Result2, Title = g.Title }).ToList()) ?? new List<ResponsePrescriptionServiceResultReport>();
            report.PatientInfo.Tests = patientcard.Tests.ToString();
            report.VitalSigns = Mapper.PrescriptionShareMapper.VitalMap(vital) ?? new VitalSignDto();
            report.PatientHistories = Mapper.PrescriptionShareMapper.PaitenInfoMap(patientHistory) ?? new PatientInfoDto();

            var answers = await _answerServiceRepository.Where(d => d.PatientId == cur.Prescriptoin.Reception.PatientId).Select(g => new LaboratoryAnswerDto { AnswerDate = g.CreateDate.ToDateTimeStringTry(), LinkAnswer = g.FileId }).ToListAsync();
            report.LaboratoryAnswers = answers;
            report.Translate = translate;

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = report
            };
        }
    }
}
