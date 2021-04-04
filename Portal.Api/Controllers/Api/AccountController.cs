using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.DTO;
using Portal.DTO.User;
using Portal.Infrastructure;
using Portal.Interface;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        private readonly IUserManagerService _userManagerService;

        public AccountController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _userManagerService.Login(loginDto);

            return new CustomActionResult(result);
        }
        /// <summary>
        /// بعد کد وریرفای در صفحه شماره موبایل
        /// </summary>
        /// <param name="requestMobileVerifyDto"></param>
        /// <returns></returns>

        [HttpPost("CheckVerify")]
        public async Task<IActionResult> CheckVerify(RequestVerifyDto requestMobileVerifyDto)
        {
            var result = await _userManagerService.CheckVerifyUser(requestMobileVerifyDto);

            return new CustomActionResult(result);
        }

        [HttpPost("ExistUser")]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> ExistUser(RequestMobileVerifyDto requestMobileVerifyDto)
        {
            var result = await _userManagerService.CheckExistUser(requestMobileVerifyDto);

            return new CustomActionResult(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _userManagerService.RegisterAsync(userRegisterDto);

            return new CustomActionResult(result);
        }

        /// <summary>
        /// ثبت یوزر مختص مراکز
        /// </summary>
        /// <param name="userRegisterDto"></param>
        /// <returns></returns>
        [HttpPost("RegisterForCenter")]
        public async Task<IActionResult> RegisterForCenter(UserCenterRegisterDto userCenterRegisterDto)
        {
            var result = await _userManagerService.RegisterForCenter(userCenterRegisterDto);

            return new CustomActionResult(result);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(RequestTokenDto  requestTokenDto)
        {
            var result = await _userManagerService.Logout(Guid.Parse(requestTokenDto.Token));

            return new CustomActionResult(result);
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> Verify( RequestVerifyDto requestVerifyDto)
        {
            var result = await _userManagerService.Verify(requestVerifyDto);

            return new CustomActionResult(result);
        }
        
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword( RequestForgetPasswordyDto requestForgetPasswordyDto)
        {
            var result = await _userManagerService.ForgetPassword(requestForgetPasswordyDto);

            return new CustomActionResult(result);
        }

        [HttpPost("ResendVerifyCode")]
        public async Task<IActionResult> ResendVerifyCode(RequestMobileVerifyDto requestVerifyDto)
        {
            var result = await _userManagerService.GenerateVerify(requestVerifyDto);

            return new CustomActionResult(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(RequestMobileVerifyDto requestVerifyDto)
        {
            var result = await _userManagerService.GenerateVerify(requestVerifyDto);

            return new CustomActionResult(result);
        }

        [HttpGet("Captcha")]
        public async Task<IActionResult> GetCaptcha()
        {
            var result = await _userManagerService.GetCaptcha();

            return new CustomActionResult(result);
        }

        [HttpPost("ChangePassword")]
        [CustomAuthorization]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto )
        {
            var result = await _userManagerService.ChangePassword(changePasswordDto);

            return new CustomActionResult(result);
        }

        [HttpPost("CheckCard")]
        public async Task<IActionResult> CheckCard(CardCodeDto cardCodeDto )
        {
            var result = await _userManagerService.CheckCard(cardCodeDto);

            return new CustomActionResult(result);
        }

        [HttpPost("RegisterForCard")]
        public async Task<IActionResult> RegisterForCard(RegisterCardDto cardCodeDto)
        {
            var result = await _userManagerService.RegisterCard(cardCodeDto);

            return new CustomActionResult(result);
        }
    }
}
