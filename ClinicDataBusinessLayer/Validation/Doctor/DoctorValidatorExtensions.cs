namespace ClinicDataBusinessLayer.Validation.Doctor;

public static class DoctorValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> DoctorId<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(Resources.DoctorMessages.KeyGreaterThanZero);
    }
    public static IRuleBuilderOptions<T, string> DoctorSpecializatio<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.DoctorMessages.SpecializationRequired)
            .MaximumLength(100)
            .WithMessage(Resources.DoctorMessages.SpecializationMaxLength);
    }

}