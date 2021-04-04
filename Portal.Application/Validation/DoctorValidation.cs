using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.DTO.Doctor;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Validation
{
    public class DoctorValidation : AbstractValidator<DoctorDto>
    {
        private readonly DbSet<Person> _personRepository;
        public DoctorValidation(IStringLocalizer<SharedResource> sharedLocalizer, DbSet<Person> personRepository = null)
        {
            RuleFor(d => d.FirstName).NotEmpty()
               .WithMessage(sharedLocalizer["DoctorForm.FieldValidation.Required.FirstName"]);//+
            
            RuleFor(d => d.LastName).NotEmpty()
               .WithMessage(sharedLocalizer["DoctorForm.FieldValidation.Required.LastName"]);//+

            RuleFor(d => d.Mobile).NotEmpty()
              .WithMessage(sharedLocalizer["DoctorForm.FieldValidation.Required.Mobile"]);//+


            //RuleFor(d => d.CenterTypeId).NotEmpty()
            //    .WithMessage(sharedLocalizer["CenterForm.FieldValidation.Required.CenterTypeId"]);


            if (personRepository != null)
            {
                _personRepository = personRepository;

                RuleFor(x => x).MustAsync((x, cancellation) => IsDuplicateMulti(x))
                                  .WithMessage(sharedLocalizer["DoctorForm.FieldValidation.Duplicate"]).OverridePropertyName("Duplicate").When(d => d.Id == 0);//+

            }

        }


        public async Task<bool> IsDuplicateMulti(DoctorDto centerDto)
        {
            if (await _personRepository.AnyAsync(d => d.Mobile == centerDto.Mobile))
            {
                return false;
            }

            return true;
        }
    }
}