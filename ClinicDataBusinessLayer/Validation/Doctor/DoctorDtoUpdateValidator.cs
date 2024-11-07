using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Doctor;
using ClinicDataBusinessLayer.Validation.Person;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Doctor
{
    public class DoctorDtoUpdateValidator : AbstractValidator<DoctorDtoUpdate>
    {
        public DoctorDtoUpdateValidator()
        {

            RuleFor(d => d.Id)
               .GreaterThan(0)
               .WithMessage(Resources.DoctorMessages.KeyGreaterThanZero);

            RuleFor(d => d.Specializatio)
                .NotEmpty()
                .WithMessage(Resources.DoctorMessages.SpecializationRequired)
                .MaximumLength(100)
                .WithMessage(Resources.DoctorMessages.SpecializationMaxLength);

            RuleFor(d => d)
               .SetValidator(p => new PersonDtoValidator());
        }

    }

}
