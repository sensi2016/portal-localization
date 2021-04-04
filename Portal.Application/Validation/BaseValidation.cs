using FluentValidation;
using His.Reception.DTO;
using Microsoft.Extensions.Localization;
using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Validation
{
    public class BaseValidation : AbstractValidator<BaseDto>
    {
        public BaseValidation(IStringLocalizer sharedLocalizer)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(sharedLocalizer["GlobalForm.FieldValidation.Title"]);//+
            RuleFor(x => x.TitleLang2).NotEmpty().WithMessage(sharedLocalizer["GlobalForm.FieldValidation.TitleLang2"]);//+
        }
    }
}
