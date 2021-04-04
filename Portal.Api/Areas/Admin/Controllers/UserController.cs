using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Infrastructure;
using Portal.Interface;
using Portal.DTO;

namespace Portal.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagerService _userManagerService;

        public UserController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost()]
        [CustomAuthorization]
        public async Task<IActionResult> Get(RequestMobileVerifyDto requestMobileVerifyDto)
        {
            var result = await _userManagerService.GetUsersByMobile(requestMobileVerifyDto.Mobile);

            return new CustomActionResult(result);
        }



    }
}
