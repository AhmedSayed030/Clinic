namespace ClinicDataBusinessLayer.Validation.Patient;

public static class PatientValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> PatientId<T>(this IRuleBuilder<T, int> ruleBuilder)  
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(Resources.PatientMessages.KeyGreaterThanZero);
    }
}