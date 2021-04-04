using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.Infrastructure;

namespace Portal.Api.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly ICenterService _centerService;

        public CenterController(ICenterService centerService)
        {
            _centerService = centerService;
        }

        [HttpGet("{id}")]
        [CustomAuthorization]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _centerService.GetById(id);

            return new CustomActionResult(result);
        }

        [HttpPost, CustomAuthorization]
        public async Task<IActionResult> Add(CenterDto centerDto)
        {
            var result = await _centerService.Add(centerDto);

            return new CustomActionResult(result);
        }

        [HttpPost("Search"), CustomAuthorization]
        public async Task<IActionResult> Search(FilterCenterDto centerDto)
        {
            var result = await _centerService.Search(centerDto);

            return new CustomActionResult(result);
        }

        [HttpPost("Upload")]
        [CustomAuthorization]
        public async Task<IActionResult> upload([FromForm]UploadLogoDto uploadLogoDto)
        {
            var result = await _centerService.UploadLogo(uploadLogoDto);

            return new CustomActionResult(result);
        }


        [HttpPut("")]
        [CustomAuthorization]
        public async Task<IActionResult> Edit(CenterDto centerDto)
        {
            var result = await _centerService.Edit(centerDto);

            return new CustomActionResult(result);
        }

        [HttpPost("SetIsActive"), CustomAuthorization]
        public async Task<IActionResult> SetIsActive(List<CenterDto> dtos)
        {
            var result = await _centerService.SetIsActive(dtos);
            return new CustomActionResult(result);
        }
    }
}
