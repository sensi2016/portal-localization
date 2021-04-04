using FluentValidation;
using Microsoft.Extensions.Localization;
using Portal.DTO;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Validation
{
    public class FilesValidation : AbstractValidator<FilesDto>, IValidator
    {
        public FilesValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(x => x.FileGroupId)
               .NotEmpty().WithMessage(sharedLocalizer["Files.FieldValidation.Required.FileGroupId"]);//+

            RuleFor(x => x.RefferKey)
              .NotEmpty().WithMessage(sharedLocalizer["Files.FieldValidation.Required.RefferKey"]);//+

            RuleFor(x => x.TableName)
               .NotEmpty().WithMessage(sharedLocalizer["Files.FieldValidation.Required.TableName"]);//+

            RuleFor(x => x.PrimeryKey)
               .NotEmpty().WithMessage(sharedLocalizer["Files.FieldValidation.Required.PrimeryKey"]);//+
        }
    }
}
