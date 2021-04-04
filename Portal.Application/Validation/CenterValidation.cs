using FluentValidation;
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
    public class CenterValidation : AbstractValidator<CenterDto>
    {
        private readonly DbSet<Center> _centerRepository;
        public CenterValidation(IStringLocalizer<SharedResource> sharedLocalizer, DbSet<Center> centerRepository = null)
        {
            RuleFor(d => d.Title).NotEmpty()
               .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.Title"]);//+

            RuleFor(d => d.Mobile).NotEmpty()
              .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.Mobile"]);//+


            RuleFor(d => d.CenterTypeId).NotEmpty()
                .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.CenterTypeId"]); //+
                        

            if (centerRepository != null)
            {
                _centerRepository = centerRepository;

                RuleFor(x => x).MustAsync((x, cancellation) => IsDuplicateMulti(x))
                                  .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Duplicate"]).OverridePropertyName("Duplicate").When(d => d.Id == 0);

            }

        }


        public async Task<bool> IsDuplicateMulti(CenterDto centerDto)
        {
            if (await _centerRepository.AnyAsync(d => d.Mobile  == centerDto.Mobile))
            {
                return false;
            }

            return true;
        }
    }
}