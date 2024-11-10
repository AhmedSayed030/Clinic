namespace ClinicDataBusinessLayer.Validation.Payment;

public static class PaymentValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> PaymentId<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(Resources.PaymentMessages.KeyGreaterThanZero);
    }
    public static IRuleBuilderOptions<T, decimal> PaymentAmount<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(-1)
            .WithMessage(Resources.PaymentMessages.AmountGreaterThanOrEqualTo);
    }
    public static IRuleBuilderOptions<T, string?> PaymentNote<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(500)
            .WithMessage(Resources.PaymentMessages.NoteMaximumLength);
    }
    public static IRuleBuilderOptions<T, string> PaymentMethod<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(100)
            .WithMessage(Resources.PaymentMessages.MethodMaximumLength);
    }
    public static IRuleBuilderOptions<T, DateTime> PaymentDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.PaymentMessages.DateNotEmpty)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage(Resources.PaymentMessages.DateLessThanOrEqualToNow);
    }


}