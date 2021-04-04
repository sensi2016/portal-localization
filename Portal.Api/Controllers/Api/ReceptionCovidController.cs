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

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionCovidController : ControllerBase
    {
        private readonly IReceptionsService _receptionService;
        private readonly IGeneralService _generalService;

        public ReceptionCovidController(IReceptionsService receptionService, IGeneralService generalService)
        {
            _receptionService = receptionService;
            _generalService = generalService;
        }

        [HttpPost("CovidAirPort/Search")]
        public async Task<IActionResult> SearchCovidAirPort(FilterCovidAirPortDto filterCovidAirPortDto)
        {
            var result = await _receptionService.SearchAirPort(filterCovidAirPortDto);
            return new CustomActionResult(result);
        }

        [HttpPost("Lab")]
        [CustomAuthorization]
        public async Task<IActionResult> AddLab(ReceptionLabDto receptionLabDto)
        {
            var result = await _receptionService.AddLab(receptionLabDto);
            return new CustomActionResult(result);
        }

    }
}
