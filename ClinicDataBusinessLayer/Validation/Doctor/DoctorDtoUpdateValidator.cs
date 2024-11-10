namespace ClinicDataBusinessLayer.Validation.Doctor;

public class DoctorDtoUpdateValidator : AbstractValidator<DoctorDtoUpdate>
{
    public DoctorDtoUpdateValidator()
    {

        RuleFor(d => d)
            .SetValidator(_ => new PersonDtoValidator());

        RuleFor(d => d.Id)
            .DoctorId();

        RuleFor(d => d.Specializatio)
            .DoctorSpecializatio();
    }

}