using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Infrastructure;
using Portal.DTO;
using Portal.DTO.User;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Validation
{
    public class RegisterUserValidation : AbstractValidator<UserRegisterDto>
    {
        private readonly DbSet<Users> _userRepository;
        private  HttpContext _httpContext;
        public RegisterUserValidation(IStringLocalizer<SharedResource> sharedLocalizer,DbSet<Users> userRepository=null,HttpContext httpContext=null)
        {

            //RuleFor(d => d.FirstName).NotEmpty()
            //   .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.FirstName"]);

            RuleFor(d => d.VerifyCode).NotEmpty()
               .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.VerifyCode"]);//+

            RuleFor(d => d.Password).NotEmpty()
              .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.Password"]);//+

            RuleFor(d => d.Mobile).NotEmpty()
             .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.Mobile"]);//+

            //RuleFor(d => d.SecurityCode).NotEmpty()
            //    .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.SecurityCode"]);

            RuleFor(x => x)
                .Must(IsCheckPassword).WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.NotRepeat"]).OverridePropertyName("ConfrimPassword");//+


            if (userRepository != null)
            {
                _userRepository = userRepository;

                RuleFor(x => x).MustAsync((x, cancellation) => IsDuplicateMulti(x))
                                  .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Duplicate"]).OverridePropertyName("Duplicate").When(d => d.Id == 0);

            }

            if (httpContext != null)
            {
                _httpContext = httpContext;

                RuleFor(x => x)
                    .Must(IsCheckSecurityCode).WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.NotTrueSecurityCode"]).OverridePropertyName("SecurityCode");//+

            }
        }

        public bool IsCheckPassword(UserRegisterDto userRegisterDto)
        {
            if (!string.IsNullOrEmpty(userRegisterDto.Password) || !string.IsNullOrEmpty(userRegisterDto.RepeatPassword))
            {
                //check password equals 
                return userRegisterDto.Password.Equals(userRegisterDto.RepeatPassword);
            }

            return true;
        }

        public bool IsCheckSecurityCode(UserRegisterDto userRegisterDto)
        {
            if (!Captcha.ValidateCaptchaCode(userRegisterDto.SecurityCode, _httpContext))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsDuplicateMulti(UserRegisterDto patientDto)
        {
            if (await _userRepository.AnyAsync(d => d.UserName==patientDto.Mobile))
            {
                return false;
            }

            return true;
        }


    }


    public class RegisterCardUserValidation : AbstractValidator<RegisterCardDto>
    {
        private readonly DbSet<Users> _userRepository;
        private HttpContext _httpContext;
        public RegisterCardUserValidation(IStringLocalizer<SharedResource> sharedLocalizer, DbSet<Users> userRepository = null, HttpContext httpContext = null)
        {

            //RuleFor(d => d.FirstName).NotEmpty()
            //   .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.FirstName"]);

            RuleFor(d => d.PasswordCard).NotEmpty()
               .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.PasswordCard"]);//+

            RuleFor(d => d.UserNameCard).NotEmpty()
               .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.UserNameCard"]);//+

            RuleFor(d => d.VerifyCode).NotEmpty()
               .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.VerifyCode"]);//+

            RuleFor(d => d.Password).NotEmpty()
              .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.Password"]);//+

            RuleFor(d => d.Mobile).NotEmpty()
             .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.Mobile"]);//+

            //RuleFor(d => d.SecurityCode).NotEmpty()
            //    .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.SecurityCode"]);

            RuleFor(x => x)
                .Must(IsCheckPassword).WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.NotRepeat"]).OverridePropertyName("ConfrimPassword");//+


            //if (userRepository != null)
            //{
            //    _userRepository = userRepository;

            //    RuleFor(x => x).MustAsync((x, cancellation) => IsDuplicateMulti(x))
            //                      .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Duplicate"]).OverridePropertyName("Duplicate").When(d => d.Id == 0);

            //}

           
        }

        public bool IsCheckPassword(RegisterCardDto userRegisterDto)
        {
            if (!string.IsNullOrEmpty(userRegisterDto.Password) || !string.IsNullOrEmpty(userRegisterDto.RepeatPassword))
            {
                //check password equals 
                return userRegisterDto.Password.Equals(userRegisterDto.RepeatPassword);
            }

            return true;
        }

    

        public async Task<bool> IsDuplicateMulti(RegisterCardDto patientDto)
        {
            if (await _userRepository.AnyAsync(d => d.UserName == patientDto.Mobile))
            {
                return false;
            }

            return true;
        }


    }
}