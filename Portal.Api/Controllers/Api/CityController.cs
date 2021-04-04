using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.Infrastructure;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService) => _cityService= cityService;

        [CustomAuthorization]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cityService.GetById(id);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cityService.GetAll();
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost("All")]
        public async Task<IActionResult> GetAllPaging(BaseRequestPagingPost paging)
        {
            var result = await _cityService.GetAll(paging);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost]
        public async Task<IActionResult> Add(CityDto dto)
        {
            var result = await _cityService.Add(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPut]
        public async Task<IActionResult> Edit(CityDto dto)
        {
            var result = await _cityService.Edit(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpDelete]
        public async Task<IActionResult> Delete(BaseRequestPost<int> dto)
        {
            var result = await _cityService.Delete(dto);
            return new CustomActionResult(result);
        }
    }
}
