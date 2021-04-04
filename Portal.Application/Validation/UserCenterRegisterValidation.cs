using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Validation
{
    class UserCenterRegisterValidation : AbstractValidator<UserCenterRegisterDto>
    {
        private readonly DbSet<Users> _userRepository;
        private HttpContext _httpContext;
        public UserCenterRegisterValidation(IStringLocalizer<SharedResource> sharedLocalizer, DbSet<Users> userRepository = null, HttpContext httpContext = null)
        {         
            RuleFor(d => d.Name).NotEmpty()
               .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.Name"]);//+

            RuleFor(d => d.UserName).NotEmpty()
               .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.UserName"]);//

            RuleFor(d => d.CenterId).NotEmpty()
              .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.CenterId"]);//+

            RuleFor(d => d.Password).NotEmpty()
              .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.Password"]).When(d=>d.Id==
              0);

            RuleFor(d => d.Mobile).NotEmpty()
                .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.Mobile"]);//+

            RuleFor(x => x)
                .Must(IsCheckPassword).WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.NotRepeat"]).OverridePropertyName("ConfrimPassword");//+


            if (userRepository != null)
            {
                _userRepository = userRepository;

                RuleFor(x => x).MustAsync((x, cancellation) => IsDuplicateMulti(x))
                                  .WithMessage(sharedLocalizer["CenterForm.FieldValidation.UserDuplicate"]).OverridePropertyName("Duplicate");//+

            }

            if (httpContext != null)
            {
                _httpContext = httpContext;

                //RuleFor(x => x)
                //    .Must(()=>IsDuplicateMulti).WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.Duplicate"]).OverridePropertyName("Duplicate");

            }
        }

        public bool IsCheckPassword(UserCenterRegisterDto userRegisterDto)
        {
            if (!string.IsNullOrEmpty(userRegisterDto.Password) || !string.IsNullOrEmpty(userRegisterDto.RepeatPassword))
            {
                //check password equals 
                return userRegisterDto.Password.Equals(userRegisterDto.RepeatPassword);
            }

            return true;
        }


        public async Task<bool> IsDuplicateMulti(UserCenterRegisterDto userDto)
        {
            if (userDto.Id == 0 && await _userRepository.AnyAsync(d => d.UserName == userDto.UserName))
            {
                return false;
            }
            else if (userDto.Id > 0 && await _userRepository.AnyAsync(d =>d.Id!= userDto.Id &&  d.UserName == userDto.UserName))
                return false;

            return true;
        }
    }
}