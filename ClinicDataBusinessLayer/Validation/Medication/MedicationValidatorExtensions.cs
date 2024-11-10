namespace ClinicDataBusinessLayer.Validation.Medication;

public static class MedicationValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> MedicationId<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(Resources.MedicationMessages.KeyGreaterThanZero);
    }
    public static IRuleBuilderOptions<T, byte> MedicationFrequency<T>(this IRuleBuilder<T, byte> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan((byte)0)
            .WithMessage(Resources.MedicationMessages.FrequencyGreaterThanZero);
    }
    public static IRuleBuilderOptions<T, string> MedicationName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.MedicationMessages.NameNotEmpty)
            .MaximumLength(100)
            .WithMessage(Resources.MedicationMessages.NameMaxLength);
    }
    public static IRuleBuilderOptions<T, string> MedicationDosage<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.MedicationMessages.DosageNotEmpty)
            .MaximumLength(100)
            .WithMessage(Resources.MedicationMessages.DosageMaxLength);
    }
    public static IRuleBuilderOptions<T, DateOnly> MedicationStartDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.MedicationMessages.StartDateInFuture)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage(Resources.MedicationMessages.StartDateInFuture);
    }
    public static IRuleBuilderOptions<T, DateOnly> MedicationEndDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.MedicationMessages.EndDateInFuture)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage(Resources.MedicationMessages.EndDateInFuture);
    }

}