namespace ClinicDataBusinessLayer.Validation.Patient;

public class PatientDtoAddValidator : AbstractValidator<PatientDtoAdd>
{
    public PatientDtoAddValidator(IValidator<IPersonDto> personValidator)
    {
        RuleFor(p => p)
            .SetValidator(personValidator);
    }
}