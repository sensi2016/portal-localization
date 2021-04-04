using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DTO.Doctor;
using Portal.Infrastructure;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly ICenterService _centerService;
        private readonly IGeneralService _generalService;
        private readonly IChartService  _chartService;

        public HomeController(ICenterService centerService, IGeneralService generalService, IChartService chartService)
        {
            _centerService = centerService;
            _generalService = generalService;
            _chartService = chartService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(FilterHomeCenterDto filterCenterDto)
        {
            var result = await _centerService.Search(filterCenterDto);
           
            return new CustomActionResult(result);
        }


        [HttpPost("app/Search")]
        public async Task<IActionResult> AppDoctorSearch(FilterHomeCenterAppDto filterDoctorHomeDto)
        {
            var result = await _centerService.SearchApp(filterDoctorHomeDto);

            return new CustomActionResult(result);
        }


        [HttpGet("Base")]
        public async Task<IActionResult> Base()
        {
            var result = await _generalService.GetBaseInfo("CenterForm");

            return new CustomActionResult(result);
        }   
                
        [HttpGet("Covid")]
        public async Task<IActionResult> Covid()
        {
            var result = await _chartService.COVID19();// ("CenterForm");

            return new CustomActionResult(result);
        }

        [HttpGet("CenterInfo/{Id}")]
        // ReSharper disable once InconsistentNaming
        public async Task<IActionResult> GetCenterInfo(int Id)
        {
            var result = await _centerService.GetCenterInfo(Id);
            return new CustomActionResult(result);
        }
    }
}
