using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.Validation.Person;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Patient
{
    public class PatientDtoAddValidator : AbstractValidator<PatientDtoAdd>
    {
        public PatientDtoAddValidator()
        {
            RuleFor(p => p)
               .SetValidator(p => new PersonDtoValidator());
        }
    }


}
