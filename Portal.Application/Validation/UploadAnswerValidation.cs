using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.DTO.Answer;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Validation
{
    public class UploadAnswerValidation : AbstractValidator<UploadAnswerDto>
    {
        private readonly DbSet<AnswerService> _centerRepository;
        public UploadAnswerValidation(IStringLocalizer<SharedResource> sharedLocalizer, DbSet<AnswerService> centerRepository = null)
        {
            RuleFor(d => d.File).NotEmpty()
               .WithMessage(sharedLocalizer["UploadAnswerForm.FieldValidation.Required.File"]);

            //RuleFor(d => d.Mobile).NotEmpty()
            //  .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.Mobile"]);


            //RuleFor(d => d.UserId).NotEmpty()
            //    .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.CenterTypeId"]);

        }
    }
}