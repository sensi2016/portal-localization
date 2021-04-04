using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.DTO.User;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Validation
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordDto>
    {
        private readonly DbSet<Users> _userRepository;
        private readonly IWorkContextService _workContext;
        public ChangePasswordValidation(IStringLocalizer<SharedResource> sharedLocalizer, DbSet<Users> userRepository = null,IWorkContextService workContext=null)
        {
            RuleFor(d => d.OldPassword).NotEmpty()
               .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.OldPassword"]);//+

            RuleFor(d => d.NewPassword).NotEmpty()
               .WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.NewPassword"]);//+


            RuleFor(x => x)
                .Must(IsCheckPassword).WithMessage(sharedLocalizer["RegisterForm.FieldValidation.Required.NotRepeat"]).OverridePropertyName("ConfrimPassword");//+

            

            if (userRepository != null)
            {
                _userRepository = userRepository;
                _workContext = workContext;
                RuleFor(x => x).MustAsync((x, cancellation) => IsCheckOldPassword(x))
                                  .WithMessage(sharedLocalizer["ChangePasswordForm.FieldValidation.NotFoundPassword"]).OverridePropertyName("NotFoundPassword");//+

            }

        }

        public bool IsCheckPassword(ChangePasswordDto changePasswordDto)
        {
            if (!string.IsNullOrEmpty(changePasswordDto.NewPassword) || !string.IsNullOrEmpty(changePasswordDto.RepeatPassword))
            {
                //check password equals 
                return changePasswordDto.NewPassword.Equals(changePasswordDto.RepeatPassword);
            }

            return true;
        }

      
        public async Task<bool> IsCheckOldPassword(ChangePasswordDto changePasswordDto )
        {
            if (await _userRepository.AnyAsync(d =>d.Id== _workContext.UserId &&!d.Password.Equals(Utilities.ComputeHashSHA256(changePasswordDto.OldPassword))))
            {
                return false;
            }

            return true;
        }
    }
}