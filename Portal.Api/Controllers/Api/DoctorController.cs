using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO.Doctor;
using Portal.Infrastructure;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IGeneralService _generalService;

        public DoctorController(IDoctorService doctorService, IGeneralService generalService)
        {
            _doctorService = doctorService;
            _generalService = generalService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(FilterDoctorHomeDto filterDoctorHomeDto)
        {
            var result = await _doctorService.SearchHome(filterDoctorHomeDto);

            return new CustomActionResult(result);
        }

        [HttpPost("app/Search")]
        public async Task<IActionResult> AppDoctorSearch(FilterDoctorAppHomeDto filterDoctorHomeDto)
        {
            var result = await _doctorService.SearchAppHome(filterDoctorHomeDto);

            return new CustomActionResult(result);
        }


        [HttpGet("{id}/DoctorInfo")]
        //[CustomAuthorization]
        public async Task<IActionResult> DoctorInfo(int id)
        {
            var result = await _doctorService.Info(id);

            return new CustomActionResult(result);

        }


        [HttpPost("Base")]
        public async Task<IActionResult> Base()
        {
            var result = await _generalService.GetBaseInfo("DoctorForm");

            return new CustomActionResult(result);
        }

        [HttpPost("SetIsActive"), CustomAuthorization]
        public async Task<IActionResult> SetIsActive(List<DoctorDto> dtos)
        {
            var result = await _doctorService.SetIsActive(dtos);
            return new CustomActionResult(result);

        }
    }
}
