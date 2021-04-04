using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DTO.City;
using Portal.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _zoneService;
        public ZoneController(IZoneService zoneService) => _zoneService= zoneService;

        [CustomAuthorization]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _zoneService.GetById(id);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _zoneService.GetAll();
            return new CustomActionResult(result);
        }

       

        [CustomAuthorization]
        [HttpPost("All")]
        public async Task<IActionResult> GetAllPaging(BaseRequestPagingPost paging)
        {
            var result = await _zoneService.GetAll(paging);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost]
        public async Task<IActionResult> Add(ZoneDto dto)
        {
            var result = await _zoneService.Add(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPut]
        public async Task<IActionResult> Edit(ZoneDto dto)
        {
            var result = await _zoneService.Edit(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpDelete]
        public async Task<IActionResult> Delete(BaseRequestPost<int> dto)
        {
            var result = await _zoneService.Delete(dto);
            return new CustomActionResult(result);
        }
    }
}
