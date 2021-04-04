using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DTO.Answer;
using Portal.Infrastructure;

namespace Portal.Api.Controllers.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerServiceService _answerServiceService;

        public AnswerController(IAnswerServiceService answerServiceService)
        {
            _answerServiceService = answerServiceService;
        }

        [HttpPost("OutAnswer")]
        public async Task<IActionResult> OutAnswer([FromForm]FastUploadAnswerDto fastUploadAnswerDto )
        {
            var result = await _answerServiceService.FastUpload(fastUploadAnswerDto);
            return new CustomActionResult(result);
        }

        [HttpPost("ExcelAnswer")]
        public async Task<IActionResult> ExcelAnswer(ExcelAnswerDto requestAnswerExcel )
        {
            var result = await _answerServiceService.ExcelUpload(requestAnswerExcel);
            return new CustomActionResult(result);
        }

        [HttpPost("ListExcelAnswer")]
        //[CustomAuthorization]
        public async Task<IActionResult> ListExcelAnswer(FilterAnswerExcelDto filterAnswerExcelDto)
        {
            var result = await _answerServiceService.ListExcelUpload(filterAnswerExcelDto);
            return new CustomActionResult(result);
        }

        [HttpGet("ReportExcelAnswer/{key}")]
        public async Task<IActionResult> ReportExcelAnswer(string key)
        {
            var result = await _answerServiceService.ReportExcelAnswer(key);
            return new CustomActionResult(result);
        }


        [HttpGet("ExcelAnswer/{id}")]
        public async Task<IActionResult> ExcelAnswerGetById(long id)
        {
            var result = await _answerServiceService.ExcelGetById(id);
            return new CustomActionResult(result);
        } 
        
        [HttpPut("ExcelAnswer")]
        public async Task<IActionResult> EditExcelAnswer(ExcelAnswerDetailsDto excelAnswerDetailsDto)
        {
            var result = await _answerServiceService.EditExcel(excelAnswerDetailsDto);
            return new CustomActionResult(result);
        }
    }
}
