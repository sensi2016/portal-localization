using FluentValidation;
using Microsoft.Extensions.Localization;
using Portal.DTO.Covid;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Validation
{
    public class ReceptionLabValidation : AbstractValidator<ReceptionLabDto>
    {
        public ReceptionLabValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            //RuleFor(d => d.FirstName).NotEmpty()
            //   .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.FirstName"]);

            //RuleFor(d => d.FatherName).NotEmpty()
            //  .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.FatherName"]);

            //RuleFor(d => d.GrandFatherName).NotEmpty()
            //  .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.GrandFatherName"]);

            //RuleFor(d => d.Mobile).NotEmpty()
            //   .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.Mobile"]);

          

        }
    }
}
