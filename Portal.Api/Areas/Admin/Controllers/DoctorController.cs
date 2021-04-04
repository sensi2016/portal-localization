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

namespace Portal.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost, CustomAuthorization]
        public async Task<IActionResult> Add(DoctorDto doctorDto)
        {
            var result = await _doctorService.Add(doctorDto);
            return new CustomActionResult(result);
        }

        [HttpPut, CustomAuthorization]
        public async Task<IActionResult> Edit(DoctorDto doctorDto)
        {
            var result = await _doctorService.Edit(doctorDto);
            return new CustomActionResult(result);
        }

        [HttpGet("{id}"), CustomAuthorization]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorService.GetById(id);
            return new CustomActionResult(result);
        }

        [HttpPost("Search"), CustomAuthorization]
        public async Task<IActionResult> Search(FilterDoctorDto filterDoctorDto)
        {
            var result = await _doctorService.Search(filterDoctorDto);
            return new CustomActionResult(result);
        }

        [HttpPost("Upload")]
        [CustomAuthorization]
        public async Task<IActionResult> upload([FromForm] UploadLogoDoctorDto uploadLogoDto)
        {
            var result = await _doctorService.UploadLogo(uploadLogoDto);

            return new CustomActionResult(result);
        }
    }
}
