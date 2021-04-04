using Portal.Application.Interface;
using Portal.Application.Interface.Base;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Portal.Application.Service
{
    public class GeneralService : IGeneralService
    {
        private readonly IBasicService<Sex> _sexService;
        private readonly IBasicService<ReceptionServiceStatus> _receptionServiceStatusService;
        private readonly IBasicService<Nationality> _nationalityService;
        private readonly IBasicService<RefferFrom> _refferFromService;
        private readonly IBasicService<GeneralStatus> _generalStatusService;
        private readonly IBasicService<SellingType> _sellingTypeService;
        private readonly IBasicService<WorkTimeType> _wrkTimeTypeService;
        private readonly IBasicService<OwnershipType> _ownershipTypeService;
        private readonly IBasicService<Rh> _rHService;
        private readonly IBasicService<Examplace> _examplaceService;
        private readonly IBasicService<Country> _countryService;
        private readonly IBasicService<VisitType> _visitTypeService;
        private readonly ICityService _cityService;
        private readonly IBasicService<Province> _proviceService;
        private readonly IBasicService<MaritalStatus> _maritalStatusService;
        private readonly IBasicService<Expertise> _expertiseService;
        private readonly IZoneService _zoneService;
        private readonly IBasicService<Scientificlevel> _scientificlevelService;
        private readonly IBasicService<Place> _placeService;
        private readonly IBasicService<SpecialIllness> _specialIllnessService;
        private readonly IBasicService<RelationShip> _relationShipService;
        private readonly IBasicService<PregnancySeason> _pregnancySeasonService;
        private readonly IBasicService<Job> _jobService;
        private readonly IBasicService<JobType> _jobTypeService;
        private readonly IBasicService<BloodGroup> _bloodGroupService;
        private readonly IBasicService<Sign> _signService;
        private readonly IBasicService<Certificate> _certificateService;
        private readonly IQuestionService _questionService;
        private readonly IUserManagerService _userService;
        private readonly IServiceService _serviceService;

        public GeneralService(IBasicService<Sex> sexService
            , IBasicService<WorkTimeType> wrkTimeTypeService
            , IBasicService<OwnershipType> ownershipTypeService
            , IBasicService<Examplace> examplaceService
            , IBasicService<Country> countryService
            , ICityService cityService
            , IBasicService<Province> proviceService
            , IZoneService zoneService
            , IBasicService<VisitType> visitTypeService
            , IBasicService<Expertise> expertiseService
            , IBasicService<Scientificlevel> scientificlevel
            , IBasicService<Place> placeService
            , IBasicService<SellingType> sellingTypeService
            , IBasicService<SpecialIllness> specialIllnessService
            , IBasicService<RelationShip> relationShipService
            , IBasicService<PregnancySeason> pregnancySeasonService
            , IBasicService<Job> jobService
            , IBasicService<JobType> jobTypeService
            , IQuestionService questionService
            , IBasicService<BloodGroup> bloodGroupService
            , IBasicService<Sign> signService
            , IBasicService<RefferFrom> refferFromService
             , IBasicService<GeneralStatus> generalStatusService
             , IBasicService<Nationality> nationalityService
             , IUserManagerService userService
             , IBasicService<ReceptionServiceStatus> receptionServiceStatusService
             , IBasicService<Rh> rHService
            , IBasicService<MaritalStatus> maritalStatusService
            , IBasicService<Certificate> certificateService
            , IServiceService serviceService)
        {
            _sexService = sexService;
            _wrkTimeTypeService = wrkTimeTypeService;
            _ownershipTypeService = ownershipTypeService;
            _examplaceService = examplaceService;
            _countryService = countryService;
            _cityService = cityService;
            _proviceService = proviceService;
            _zoneService = zoneService;
            _visitTypeService = visitTypeService;
            _expertiseService = expertiseService;
            _scientificlevelService = scientificlevel;
            _placeService = placeService;
            _sellingTypeService = sellingTypeService;
            _specialIllnessService = specialIllnessService;
            _relationShipService = relationShipService;
            _pregnancySeasonService = pregnancySeasonService;
            _jobService = jobService;
            _jobTypeService = jobTypeService;
            _questionService = questionService;
            _signService = signService;
            _bloodGroupService = bloodGroupService;
            _refferFromService = refferFromService;
            _generalStatusService = generalStatusService;
            _nationalityService = nationalityService;
            _userService = userService;
            _receptionServiceStatusService = receptionServiceStatusService;
            _rHService = rHService;
            _maritalStatusService = maritalStatusService;
            _serviceService = serviceService;
            _certificateService = certificateService;
        }

        public async Task<BaseResponseDto> GetBaseInfo(string pageName)
        {
            var dic = new Dictionary<string, object>();

            if (pageName == "RegisterForm")
            {
                dic.Add("sexs", (await _sexService.GetAll()).Data);
            }


            if (pageName == "CenterForm")
            {
                dic.Add("wrkTimeTypes", (await _wrkTimeTypeService.GetAll()).Data);
                dic.Add("ownershipTypes", (await _ownershipTypeService.GetAll()).Data);
                dic.Add("examplaces", (await _examplaceService.GetAll()).Data);
                dic.Add("countries", (await _countryService.GetAll()).Data);
                dic.Add("cities", (await _cityService.GetAll()).Data);
                dic.Add("provinces", (await _proviceService.GetAll()).Data);
                dic.Add("zones", (await _zoneService.GetAll()).Data);
                dic.Add("places", (await _placeService.GetAll()).Data);
                dic.Add("sellingTypes", (await _sellingTypeService.GetAll()).Data);
                dic.Add("expertises", (await _expertiseService.GetAll()).Data);
                dic.Add("sexs", (await _sexService.GetAll()).Data);
                dic.Add("rh", (await _rHService.GetAll()).Data);
                dic.Add("bloodGroups", (await _bloodGroupService.GetAll()).Data);
                dic.Add("maritalStatuss", (await _maritalStatusService.GetAll()).Data);
                dic.Add("centerServices", (await _serviceService.ByParentLocalCode("PortalHospital")).Data);
            }

            if (pageName == "DoctorForm")
            {
                dic.Add("wrkTimeTypes", (await _wrkTimeTypeService.GetAll()).Data);
                dic.Add("visitTypes", (await _visitTypeService.GetAll()).Data);
                dic.Add("ownershipTypes", (await _ownershipTypeService.GetAll()).Data);
                dic.Add("examplaces", (await _examplaceService.GetAll()).Data);
                dic.Add("countries", (await _countryService.GetAll()).Data);
                dic.Add("cities", (await _cityService.GetAll()).Data);
                dic.Add("provinces", (await _proviceService.GetAll()).Data);
                dic.Add("zones", (await _zoneService.GetAll()).Data);
                dic.Add("expertises", (await _expertiseService.GetAll()).Data);
                dic.Add("scientificlevels", (await _scientificlevelService.GetAll()).Data);
                dic.Add("certificates", (await _certificateService.GetAll()).Data);

            }

            if (pageName == "ListAnswerExcelForm")
            {
                dic.Add("generalStatuses", (await _generalStatusService.GetAll()).Data);
                dic.Add("refferFroms", (await _refferFromService.GetAll()).Data);
            }

            if (pageName == "ReceptionCovidForm")
            {
                dic.Add("specialIllnesses", (await _specialIllnessService.GetAll()).Data);
                dic.Add("relationShips", (await _relationShipService.GetAll()).Data);
                dic.Add("sexs", (await _sexService.GetAll()).Data);
                dic.Add("pregnancySeasons", (await _pregnancySeasonService.GetAll()).Data);
                dic.Add("jobs", (await _jobService.GetAll()).Data);
                dic.Add("jobTypes", (await _jobTypeService.GetAll()).Data);
                dic.Add("cities", (await _cityService.GetAll()).Data);
                dic.Add("provinces", (await _proviceService.GetAll()).Data);
                dic.Add("zones", (await _zoneService.GetAll()).Data);
                dic.Add("questions", (await _questionService.GetQuestionAndAnswer()).Data);
                dic.Add("signs", (await _signService.GetAll()).Data);
                dic.Add("bloodGroups", (await _bloodGroupService.GetAll()).Data);
                dic.Add("expertises", (await _expertiseService.GetAll()).Data);
            }

            if (pageName == "ReceptionLabForm")
            {
                dic.Add("sexs", (await _sexService.GetAll()).Data);
                var nationalities = (List<BaseDto>)(await _nationalityService.GetAll()).Data;
                nationalities.ForEach(d => d.Title = Utilities.AppendText(d.TitleLang2, d.Title, "-"));

                dic.Add("nationalities", nationalities);
            }

            if (pageName == "ReceptionLabSearchForm")
            {
                dic.Add("sexs", (await _sexService.GetAll()).Data);

                var nationalities = (List<BaseDto>)(await _nationalityService.GetAll()).Data;
                nationalities.ForEach(d => d.Title = Utilities.AppendText(d.TitleLang2, d.Title, "-"));

                dic.Add("nationalities", nationalities);
                dic.Add("users", (await _userService.GetAllbyCurrentSection()).Data);
                dic.Add("status", (await _receptionServiceStatusService.GetAll()).Data);
            }

            return new BaseResponseDto
            {
                Data = dic,
                Status = ResponseStatus.Success
            };

        }
    }
}
