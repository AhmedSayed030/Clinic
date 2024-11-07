using ClinicDataBusinessLayer.DTOs.Doctor;
using ClinicDataBusinessLayer.Validation.Person;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Doctor
{
    public class DoctorDtoAddValidator : AbstractValidator<DoctorDtoAdd>
    {
        public DoctorDtoAddValidator()
        {
            RuleFor(d => d)
                .SetValidator(p => new PersonDtoValidator());

            RuleFor(d => d.Specializatio)
                .NotEmpty()
                .WithMessage(Resources.DoctorMessages.SpecializationRequired)
                .MaximumLength(100)
                .WithMessage(Resources.DoctorMessages.SpecializationMaxLength);

        }
    }
}
