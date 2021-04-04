using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DTO.Answer;
using Portal.Infrastructure;

namespace Portal.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerServiceService _answerServiceService;

        public AnswerController(IAnswerServiceService answerServiceService)
        {
            _answerServiceService = answerServiceService;
        }

        [HttpPost("Upload")]
        [CustomAuthorization]
        public async Task<IActionResult> Upload([FromForm] UploadAnswerDto uploadAnswerDto)
        {
            var result = await _answerServiceService.Upload(uploadAnswerDto);

            return new CustomActionResult(result);
        }


        [HttpGet("{fileId}Download")]
        public async Task<IActionResult> Download(string fileId)
        {
            var result = await _answerServiceService.Download(fileId);
            return new CustomActionResult(result);

        }

        [HttpPost("Sms")] 
        [CustomAuthorization]
        public async Task<IActionResult> Sms(NotificationDto notificationDto )
        {
            var result = await _answerServiceService.Sms(notificationDto);

            return new CustomActionResult(result);
        }

        [HttpPost("Email")]
        [CustomAuthorization]
        public async Task<IActionResult> Email(NotificationDto   notificationDto)
        {
            var result = await _answerServiceService.Email(notificationDto);

            return new CustomActionResult(result);
        }

        [HttpPost("Search")]
        [CustomAuthorization]
        public async Task<IActionResult> Search(FilterAnswerDto filterAnswer)
        {
            var result = await _answerServiceService.Search(filterAnswer);

            return new CustomActionResult(result);
        }


        [HttpPost("{userId}/Detail")]
        [CustomAuthorization]
        public async Task<IActionResult> details(int userId)
        {
            var result = await _answerServiceService.GetDetail(userId);

            return new CustomActionResult(result);
        }
    }
}
