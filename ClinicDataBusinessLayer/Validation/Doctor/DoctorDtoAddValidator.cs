namespace ClinicDataBusinessLayer.Validation.Doctor;

public class DoctorDtoAddValidator : AbstractValidator<DoctorDtoAdd>
{
    public DoctorDtoAddValidator()
    {
        RuleFor(d => d)
            .SetValidator(_ => new PersonDtoValidator());

        RuleFor(d => d.Specializatio)
            .DoctorSpecializatio();
    }
}