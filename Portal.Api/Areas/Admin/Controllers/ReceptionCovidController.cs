using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DTO.Covid;
using Portal.DTO.Message;
using Portal.Infrastructure;

namespace Portal.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]

    [ApiController]
    public class ReceptionCovidController : ControllerBase
    {
        private readonly IReceptionsService _receptionService;
        private readonly IGeneralService _generalService;
        private readonly IDashboardService _dashboardService ;

        public ReceptionCovidController(IReceptionsService receptionService, IGeneralService generalService, IDashboardService dashboardService)
        {
            _receptionService = receptionService;
            _generalService = generalService;
            _dashboardService = dashboardService;
        }

        [HttpGet("{id}")]
        [CustomAuthorization]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _receptionService.Get(id);
            return new CustomActionResult(result);
        }

        [HttpGet("Dashboard")]
        [CustomAuthorization]
        public async Task<IActionResult> Dashboard()
        {
            var result = await _dashboardService.Get();
            return new CustomActionResult(result);

        }

        [HttpGet("Base")]
        [CustomAuthorization]
        public async Task<IActionResult> GetBase()
        {
            var result = await _generalService.GetBaseInfo("ReceptionCovidForm");
            return new CustomActionResult(result);

        }


        [HttpGet("{receptionId}/BaseInfoAnswer")]
        [CustomAuthorization]
        public async Task<IActionResult> BaseInfoAnswer(long receptionId)
        {
            var result = await _receptionService.BaseInfoAnswer(receptionId);
            return new CustomActionResult(result);

        }

        [HttpPost()]
        [CustomAuthorization]
        public async Task<IActionResult> Add(ReceptionCovidDto receptionCovidDto)
        {
            var result = await _receptionService.AddCovid(receptionCovidDto);
            return new CustomActionResult(result);

        }


        [HttpPost("Search")]
        [CustomAuthorization]
        public async Task<IActionResult> Search(FilterReceptionCovidDto filterReceptionCovidDto)
        {
            var result = await _receptionService.Search(filterReceptionCovidDto);
            return new CustomActionResult(result);

        }


        [HttpPost("SetAnswer")]
        [CustomAuthorization]
        public async Task<IActionResult> SetAnswer(SetAnswerCovidDto setAnswerCovidDto)
        {
            var result = await _receptionService.SetAnswer(setAnswerCovidDto);
            return new CustomActionResult(result);
        }



        [HttpPost("CovidAirPort")]
        [CustomAuthorization]
        public async Task<IActionResult> AddCovidAirPort(ReceptionCovidAirPortDto receptionCovidAirPortDto)
        {
            var result = await _receptionService.AddCovidAirPort(receptionCovidAirPortDto);
            return new CustomActionResult(result);
        }

        [HttpPut("CovidAirPort/Init")]
        [CustomAuthorization]
        public async Task<IActionResult> EditCovidAirPort(ReceptionCovidAirPortDto receptionCovidAirPortDto)
        {
            var result = await _receptionService.EditCovidAirPort(receptionCovidAirPortDto, true);
            return new CustomActionResult(result);
        }


        [HttpPost("CovidAirPort/SendSms")]
        [CustomAuthorization]
        public async Task<IActionResult> SendSms(ReceptionIdDto receptionIdDto)
        {
            var result = await _receptionService.ResendSendSms(receptionIdDto.ReceptionId);
            return new CustomActionResult(result);
        }

        [HttpPost("CovidAirPort/Receipt")]
        [CustomAuthorization]
        public async Task<IActionResult> Receipt(ReceptionIdDto receptionIdDto)
        {
            var result = await _receptionService.ReceiptPrint(receptionIdDto.ReceptionId);
            return new CustomActionResult(result);
        }

        [HttpPost("CovidAirPort/VerifyCode")]
        [CustomAuthorization]
        public async Task<IActionResult> VerifyCode(VerifyReceptionIdDto verifyReceptionIdDto)
        {
            var result = await _receptionService.VerifyCode(verifyReceptionIdDto);
            return new CustomActionResult(result);
        }


        [HttpPost("CovidAirPort/Skip")]
        [CustomAuthorization]
        public async Task<IActionResult> Skip(ReceptionIdDto receptionIdDto)
        {
            var result = await _receptionService.Skip(receptionIdDto);
            return new CustomActionResult(result);
        }

        [HttpPost("CovidAirPort/Search")]
        [CustomAuthorization]
        public async Task<IActionResult> SearchCovidAirPort(FilterCovidAirPortDto filterCovidAirPortDto)
        {
            var result = await _receptionService.SearchAirPort(filterCovidAirPortDto);
            return new CustomActionResult(result);
        }

        [HttpPost("CovidAirPort/{receptionId}/ReportAnswse")]
        [CustomAuthorization]
        public async Task<IActionResult> AnswerPrint(long receptionId)
        {
            var result = await _receptionService.AnswerPrint(receptionId);
            return new CustomActionResult(result);
        }

        [HttpPost("CovidAirPort/UploadLogo")]
        [CustomAuthorization]
        public async Task<IActionResult> UploadLogo([FromForm] BaseUploadFileDto<long> baseUploadFileDto)
        {
            var result = await _receptionService.UploadFile(baseUploadFileDto);
            return new CustomActionResult(result);
        }
        
        [HttpPost("CovidAirPort/AnswerCovidSms")]
        [CustomAuthorization]
        public async Task<IActionResult> AnswerCovidSms(ReceptionIdDto receptionIdDto)
        {
            var result = await _receptionService.AnswerCovidSms(receptionIdDto);
            return new CustomActionResult(result);
        }

        [HttpPost("CovidAirPort/ExistPerson")]
        [CustomAuthorization]
        public async Task<IActionResult> ExistPerson(RequestMobileVerifyDto requestMobileVerifyDto )
        {
            var result = await _receptionService.CheckByMobile(requestMobileVerifyDto);
            return new CustomActionResult(result);
        }
    }
}
