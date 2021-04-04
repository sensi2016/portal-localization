using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Portal.DTO.User;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Application.Validation
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        private readonly IOptions<RequestLocalizationOptions> _locOption;
        public LoginValidation(IStringLocalizer<SharedResource> sharedLocalizer, IOptions<RequestLocalizationOptions> locOption = null)
        {
           // RuleFor(x => x.Language).NotNull().NotEmpty().WithMessage(sharedLocalizer["LoginForm.FieldValidation.Required.Language"]);//+
            //RuleFor(x => x.Password)
            //    .NotEmpty().WithMessage(sharedLocalizer["LoginForm.FieldValidation.Required.Password"])//+
            //    .MinimumLength(6).WithMessage(sharedLocalizer["LoginForm.FieldValidation.MinimumLength.Password"]);//+

            RuleFor(x => x.UserName).NotEmpty().WithMessage(sharedLocalizer["LoginForm.FieldValidation.Required.UserName"]);//+

            if (locOption != null)
            {
                _locOption = locOption;

              //  RuleFor(x => x.Language).Must(IsCulture).WithMessage(sharedLocalizer["LoginForm.FieldValidation.NotFound.Language"]);
            }
        }

        private bool IsCulture(string lang)
        {
            var cultures = _locOption.Value.SupportedCultures.ToList();

            if (cultures.Any(d => d.Name == lang))
            {
                return true;
            }

            return false;
        }
    }
}