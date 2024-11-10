namespace ClinicDataBusinessLayer.Validation.MedicalRecord;

public static class MedicalRecordValidatorExtensions
{
    public static IRuleBuilderOptions<T, int>  MedicalRecordId<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(Resources.MedicalRecordMessages.KeyGreaterThanZero);
    }
    public static IRuleBuilderOptions<T, string> MedicalRecordDiagnosis<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.MedicalRecordMessages.DiagnosisNotEmpty)
            .MaximumLength(100)
            .WithMessage(Resources.MedicalRecordMessages.DiagnosisMaxLength);
    }
    public static IRuleBuilderOptions<T, string> MedicalRecordDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.MedicalRecordMessages.DescriptionNotEmpty)
            .MaximumLength(500)
            .WithMessage(Resources.MedicalRecordMessages.DiagnosisMaxLength);
    }



}