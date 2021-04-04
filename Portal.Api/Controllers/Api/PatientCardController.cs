using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientCardController : ControllerBase
    {
        private readonly IPatientCardService _patientCardService;
        public PatientCardController(IPatientCardService patientCardService) => _patientCardService = patientCardService;

        [HttpPost("ListPrescription")]
        [CustomAuthorization]

        public async Task<IActionResult> ListPrescription(RequestPatientCardDto requestPatientCardDto)
        {
            var result = await _patientCardService.ListPrescription(requestPatientCardDto);
            return new CustomActionResult(result);
        }

        [HttpPost("ListPrescription/CurrentUser")]
        [CustomAuthorization]

        public async Task<IActionResult> ListPrescriptionCurrentUser(RequestPatientCardDto requestPatientCardDto)
        {
            var result = await _patientCardService.ListPrescription(requestPatientCardDto, true);
            return new CustomActionResult(result);
        }

        [HttpGet("Prescription/{id}")]
        public async Task<IActionResult> PrescriptionInfo(long id)
        {
            var requestParameters = new BaseRequestPost<long> {PageNumber = 1, PageSize = 100, Id = id};
            var result = await _patientCardService.PrescriptionInfo(requestParameters);
            return new CustomActionResult(result);
        }

        [HttpGet("PrescriptionByShareId/{Id}")]
        // ReSharper disable once InconsistentNaming
        public async Task<IActionResult> PrescriptionByShareId(string Id)
        {
            var result = await _patientCardService.PrescriptionByShareId(Id);
            return new CustomActionResult(result);
        }

        [HttpPost("Prescription/Share")]
        [CustomAuthorization]
        public async Task<IActionResult> Share(BaseRequestPost<long> baseRequestPost)
        {
            var result = await _patientCardService.SetShare(baseRequestPost.Id);
            return new CustomActionResult(result);
        }

        [HttpGet("Prescription/Share/{FileName}")]
        [CustomAuthorization]
        // ReSharper disable once InconsistentNaming
        public async Task<IActionResult> GetShare(string FileName)
        {
            var result = await _patientCardService.GetShare(FileName);
            return new CustomActionResult(result);
        }

        [HttpGet("Prescription/Radilogy/CurrentUser")]
        [CustomAuthorization]
        public async Task<IActionResult> RadilogyCurrentUser()
        {
            var result = await _patientCardService.ListCurrentRadilogy();

            return new CustomActionResult(result);
        }

        [HttpGet("Prescription/Radilogy/{id}")]
        [CustomAuthorization]
        public async Task<IActionResult> RadilogyCurrentUser(long id)
        {
            var result = await _patientCardService.CurrentRadilogyById(id);
            return new CustomActionResult(result);
        }

        [HttpGet("Prescription/RadiologyByShareId/{Id}")]
        // ReSharper disable once InconsistentNaming
        public async Task<IActionResult> RadiologyByShareId(string Id)
        {
            var result = await _patientCardService.RadiologyByShareId(Id);
            return new CustomActionResult(result);
        }

    }
}
