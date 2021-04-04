using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.Infrastructure;

namespace Portal.Api.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService _generalService;

        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        [CustomAuthorization]
        [HttpGet("BaseInfo/{pageName}")]
        public async Task<IActionResult> Get(string pageName)
        {
            var result =await _generalService.GetBaseInfo(pageName);

            return new CustomActionResult(result);
        }

    }
}
