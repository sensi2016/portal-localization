using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.DTO.Doctor;
using Portal.Entities.Models;
using Portal.Infrastructure;

namespace Portal.Application.Validation
{
    public class SyncDoctorValidation : AbstractValidator<SyncDoctorDto>
    {
        private readonly DbSet<Person> _personRepository;
        private readonly DbSet<Users> _userRepository;

        public SyncDoctorValidation(IStringLocalizer<SharedResource> sharedLocalizer, DbSet<Person> personRepository = null, DbSet<Users> userRepository = null)
        {
            RuleFor(x => x.UserName ).NotEmpty().NotNull().WithMessage(sharedLocalizer["SyncDoctorForm.FieldValidation.Required.UserName"]);//**
            RuleFor(x => x.Password ).NotEmpty().NotNull().WithMessage(sharedLocalizer["SyncDoctorForm.FieldValidation.Required.Password"]);//**
            RuleFor(x => x.Email ).NotEmpty().NotNull().WithMessage(sharedLocalizer["SyncDoctorForm.FieldValidation.Required.Email"]);//**

            if (personRepository != null)
            {
                _personRepository = personRepository;
                RuleFor(x => x).MustAsync((x, y) => IsNotDuplicateEmail(x))
                    .WithMessage(sharedLocalizer["SyncDoctorForm.FieldValidation.DuplicateEmail"]).OverridePropertyName("DuplicateEmail");//+

            }

            if (userRepository != null)
            {
                _userRepository = userRepository;
                RuleFor(x => x).MustAsync((x, y) => IsNotDuplicateUserName(x))
                    .WithMessage(sharedLocalizer["SyncDoctorForm.FieldValidation.DuplicateUserName"]).OverridePropertyName("DuplicateUserName");//+

            }

        }

        public async Task<bool> IsNotDuplicateEmail(SyncDoctorDto dto) => !await _personRepository.AnyAsync(x => x.Email == dto.Email);
        public async Task<bool> IsNotDuplicateUserName(SyncDoctorDto dto)=>!await _userRepository.AnyAsync(x => x.UserName==dto.UserName);
    }
}
