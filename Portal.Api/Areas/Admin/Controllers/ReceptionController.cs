using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO.Covid;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Api.Controllers.Api
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]

    [ApiController]
    public class ReceptionController : ControllerBase
    {
        private readonly IReceptionsService _receptionService;
        private readonly IGeneralService _generalService;

        public ReceptionController(IReceptionsService receptionService, IGeneralService generalService)
        {
            _receptionService = receptionService;
            _generalService = generalService;
        }


        [HttpGet("{id}/Lab")]
        [CustomAuthorization]
        public async Task<IActionResult> GetLabById(long id)
        {
            var result = await _receptionService.GetLabById(id);
            return new CustomActionResult(result);
        }

        [HttpPost("Lab")]
        [CustomAuthorization]
        public async Task<IActionResult> AddLab(ReceptionLabDto receptionLabDto)
        {
            var result = await _receptionService.AddLab(receptionLabDto);
            return new CustomActionResult(result);
        }

        [HttpPut("Lab")]
        [CustomAuthorization]
        public async Task<IActionResult> EditLab(ReceptionLabDto receptionLabDto)
        {
            var result = await _receptionService.EditLab(receptionLabDto);
            return new CustomActionResult(result);
        }

        [HttpPost("Lab/Search")]
        [CustomAuthorization]
        public async Task<IActionResult> SearchLab(FilterReceptionLabDto receptionLabDto)
        {
            var result = await _receptionService.SearchLab(receptionLabDto);
            return new CustomActionResult(result);
        }

        [HttpPost("Lab/SetAnswer")]
        [CustomAuthorization]
        public async Task<IActionResult> SetAnswer(AnswerLabDto answerLabDto)
        {
            var result = await _receptionService.SetLabAnswer(answerLabDto);
            return new CustomActionResult(result);
        }

        [HttpGet("{id}/Lab/CovidReport")]
        [CustomAuthorization]
        public async Task<IActionResult> LabCovidReport(long id)
        {
            var result = await _receptionService.LabCovidReport(id);
            return new CustomActionResult(result);
        }

    }
}
