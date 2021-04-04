using System.Threading.Tasks;
using His.Reception.Api.Infrastructure;
using His.Reception.Application.Interface;
using His.Reception.DTO;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IPcrReportService _pcrReportService;
        public ReportController(IReportService reportService, IPcrReportService pcrReportService) { _reportService = reportService; _pcrReportService = pcrReportService; }

        [CustomAuthorization]
        [HttpPost]
        public async Task<IActionResult> ReportDate(ReportDto report)
        {
            var result = await _reportService.ReportDate(report);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost("PcrReport")]
        public async Task<IActionResult> PcrReport(PcrReportParameterDto dto)
        {
            var result = await _pcrReportService.PcrReport(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost("PcrRefferFromReport")]
        public async Task<IActionResult> PcrRefferFromReport(PcrReportDateParameterDto dto)
        {
            var result = await _pcrReportService.PcrRefferFromReport(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost("PcrTestResultReport")]
        public async Task<IActionResult> PcrTestResultReport(PcrReportDateParameterDto dto)
        {
            var result = await _pcrReportService.PcrTestResultReport(dto);
            return new CustomActionResult(result);
        }

    }
}