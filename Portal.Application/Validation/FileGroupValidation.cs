using FluentValidation;
using Microsoft.Extensions.Localization;
using Portal.DTO;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Validation
{
    public class FileGroupValidation : AbstractValidator<FileGroupDto>, IValidator
    {
        public FileGroupValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage(sharedLocalizer["Files.FieldValidation.Required.FileGroupId"]);//+

            RuleFor(x => x.ParentId)
              .NotEmpty().WithMessage(sharedLocalizer["Files.FieldValidation.Required.RefferKey"]);//+

        }
    }
}
