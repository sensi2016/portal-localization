using FluentValidation;
using Microsoft.Extensions.Localization;
using Portal.Infrastructure;
using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Validation
{
    public class MenuGetVlidation : AbstractValidator<MenuGetDto>
    {
        public MenuGetVlidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(d => d.UserId).GreaterThan(0)
                .WithMessage(sharedLocalizer["GlobalForm.FieldValidation.GreaterThan.UserId"]);//+

            RuleFor(d => d.SectionId).GreaterThan(0)
                .WithMessage(sharedLocalizer["GlobalForm.FieldValidation.GreaterThan.SectionId"]);//+
        }
    }
}
