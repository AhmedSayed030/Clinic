namespace ClinicDataBusinessLayer.Validation.Prescription;

public static class PrescriptionValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> PrescriptionId<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(Resources.PaymentMessages.KeyGreaterThanZero);
    }
    public static IRuleBuilderOptions<T, string?> PrescriptionNote<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(500)
            .WithMessage(Resources.PrescriptionMessages.NoteMaxLength);
    }

}