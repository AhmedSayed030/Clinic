namespace ClinicDataBusinessLayer.Validation.Patient;

public class PatientDtoUpdateValidator : AbstractValidator<PatientDtoUpdate>
{
    public PatientDtoUpdateValidator(IValidator<IPersonDto> personValidator)
    {
        RuleFor(p => p.Id)
            .PatientId();

        RuleFor(p => p)
            .SetValidator(personValidator);
    }
}