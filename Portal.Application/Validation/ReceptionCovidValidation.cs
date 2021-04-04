using FluentValidation;
using Microsoft.Extensions.Localization;
using Portal.DTO.Covid;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Validation
{
    public class ReceptionCovidValidation : AbstractValidator<ReceptionCovidDto>
    {
        public ReceptionCovidValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(d => d.FirstName).NotEmpty()
               .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.FirstName"]);

            RuleFor(d => d.FatherName).NotEmpty()
              .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.FatherName"]);

            RuleFor(d => d.GrandFatherName).NotEmpty()
              .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.GrandFatherName"]);

            RuleFor(d => d.Mobile).NotEmpty()
               .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.Mobile"]);

            RuleFor(d => d.RelationId).NotEmpty()
               .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.RelationId"]);

        }
    }

    public class ReceptionCovidAirPortValidation : AbstractValidator<ReceptionCovidAirPortDto>
    {
        public ReceptionCovidAirPortValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(d => d.FirstName).NotEmpty()
               .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.FirstName"]);

            RuleFor(d => d.FatherName).NotEmpty()
              .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.FatherName"]);

            //RuleFor(d => d.GrandFatherName).NotEmpty()
            //  .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.GrandFatherName"]);

            RuleFor(d => d.Mobile).NotEmpty()
               .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.Mobile"]);

            RuleFor(d => d.RelationId).NotEmpty()
               .WithMessage(sharedLocalizer["ReceptionCovidForm.FieldValidation.Required.RelationId"]);

        }
    }
}