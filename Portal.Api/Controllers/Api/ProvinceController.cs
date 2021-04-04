using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.Application.Service;
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
    public class ProvinceController : ControllerBase
    {
        private readonly IZoneService _zoneService;
        private readonly IProvinceService _provinceService;

        public ProvinceController(IZoneService zoneService, IProvinceService provinceService)
        {
            _zoneService = zoneService;
            _provinceService = provinceService;
        }

        [HttpGet("{id}/Zone")]
        public async Task<IActionResult> GetAllByProvince(int id)
        {
            var result = await _zoneService.GetByProvinceId(id);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _provinceService.GetById(id);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _provinceService.GetAll();
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost("All")]
        public async Task<IActionResult> GetAllPaging(BaseRequestPagingPost paging)
        {
            var result = await _provinceService.GetAll(paging);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost]
        public async Task<IActionResult> Add(ProvinceDto dto)
        {
            var result = await _provinceService.Add(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPut]
        public async Task<IActionResult> Edit(ProvinceDto dto)
        {
            var result = await _provinceService.Edit(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpDelete]
        public async Task<IActionResult> Delete(BaseRequestPost<int> dto)
        {
            var result = await _provinceService.Delete(dto);
            return new CustomActionResult(result);
        }
    }

}

