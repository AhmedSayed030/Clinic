using ClinicDataAccessLayer.Data;
using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.Validation.Person;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Patient
{
    public class PatientDtoUpdateValidator : AbstractValidator<PatientDtoUpdate>
    {
        public PatientDtoUpdateValidator()
        {
            RuleFor(p => p.Id)
               .GreaterThan(0)
               .WithMessage(Resources.PatientMessages.KeyGreaterThanZero);

            RuleFor(p => p)
                .SetValidator(p => new PersonDtoValidator());
        }
    }


}
