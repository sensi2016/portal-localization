using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.Infrastructure;
using Portal.Interface;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IAnswerServiceService _answerServiceService;
        private readonly IUserManagerService _userManagerService;
        private readonly IPatientService _patientService;

        public PatientController(IAnswerServiceService answerServiceService, IUserManagerService userManagerService, IPatientService patientService)
        {
            _answerServiceService = answerServiceService;
            _userManagerService = userManagerService;
            _patientService = patientService;
        }

        [HttpPost("Answer")]
        [CustomAuthorization]
        public async Task<IActionResult> SearchForPatient(FilterPatinetAnswerDto filterPatinetAnswerDto)
        {
            var result = await _answerServiceService.SearchForPatient(filterPatinetAnswerDto);
            return new CustomActionResult(result);
        }


        [HttpPost("Answer/CurrentPatient")]
        [CustomAuthorization]
        public async Task<IActionResult> CurrentPatient(FilterPatinetAnswerDto filterPatinetAnswerDto)
        {
            var result = await _answerServiceService.CurrentPaitent(filterPatinetAnswerDto);
            return new CustomActionResult(result);
        }

        [HttpPost("CovidAnswer")]
        [CustomAuthorization]
        public async Task<IActionResult> CovidAnswer()
        {
            var result = await _answerServiceService.GetCovidResult();
            return new CustomActionResult(result);
        }


        [HttpGet("Profile")]
        [CustomAuthorization]
        public async Task<IActionResult> Info()
        {
            var result = await _userManagerService.CurrentUser();
            return new CustomActionResult(result);
        }

        [HttpPut("Profile")]
        [CustomAuthorization]
        public async Task<IActionResult> Edit(UserPersonDto userPersonDto)
        {
            var result = await _userManagerService.UpdateCurrentUser(userPersonDto);
            return new CustomActionResult(result);
        }

        [HttpPut("App/Profile")]
        [CustomAuthorization]
        public async Task<IActionResult> appEdit(UserPersonDto userPersonDto)
        {
            var result = await _userManagerService.UpdateCurrentUser(userPersonDto);
            return new CustomActionResult(result);
        }

        [HttpPost("Profile/Upload")]
        [CustomAuthorization]
        public async Task<IActionResult> Upload([FromForm] BaseUploadFileDto<long> baseUploadFileDto)
        {
            var result = await _userManagerService.UploadImage(baseUploadFileDto);
            return new CustomActionResult(result);
        }

        [HttpGet("{fileId}/Download")]
        [CustomAuthorization]
        public async Task<IActionResult> Download(string fileId)
        {
            var result = await _answerServiceService.Download(fileId);
            return new CustomActionResult(result);
        }

        [HttpPost("ActiveUserCard")]
        [CustomAuthorization]
        public async Task<IActionResult> ActiveUserCard(ActiveCardCodeDto activeCardCodeDto) 
            => new CustomActionResult(await _userManagerService.UserCardActive(activeCardCodeDto));


        [HttpPost("UploadFiles")]
        [CustomAuthorization]
        public async Task<IActionResult> UploadFiles([FromForm] RequestUploadFilesDto requestUploadFilesDto) 
            => new CustomActionResult(await _patientService.UploudFiles(requestUploadFilesDto));

        [HttpGet("UploadFiles/{fileGroupCode}")]
        [CustomAuthorization]
        public async Task<IActionResult> UploadFiles(string fileGroupCode) => new CustomActionResult(await _patientService.GetFiles(fileGroupCode));

        [HttpGet("UploadFiles/ListGroupFile")]
        [CustomAuthorization]
        public async Task<IActionResult> ListGroupFile() => new CustomActionResult(await _patientService.ListGroupFile());

        [HttpPost("SearchFile")]
        [CustomAuthorization]
        public async Task<IActionResult> SearchFile(SearchFileDto dto) => new CustomActionResult(await _patientService.SearchFile(dto));

        [HttpPost("ChangeGroupOfFiles")]
        [CustomAuthorization]
        public async Task<IActionResult> SearchFile(List<ChangeFileGroupDto> dtos) => new CustomActionResult(await _patientService.ChangeGroupOfFiles(dtos));
    }
}
