using FluentValidation;
using Microsoft.Extensions.Localization;
using Portal.DTO;
using Portal.Infrastructure;
namespace Portal.Application.Validation
{
    public class ProfileValidation : AbstractValidator<UserPersonDto>
    {
        public ProfileValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(sharedLocalizer["DoctorForm.FieldValidation.Required.FirstName"]);//+
            RuleFor(x => x.FatherName).NotEmpty().WithMessage(sharedLocalizer["DoctorForm.FieldValidation.Required.FatherName"]);//+
        }
    }
}