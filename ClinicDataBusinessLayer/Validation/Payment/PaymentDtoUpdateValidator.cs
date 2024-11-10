namespace ClinicDataBusinessLayer.Validation.Payment;

public class PaymentDtoUpdateValidator : AbstractValidator<PaymentDtoUpdate>
{
    public PaymentDtoUpdateValidator()
    {

        RuleFor(a => a.Id)
            .PaymentId();

        RuleFor(a => a.Amount)
            .PaymentAmount();

        RuleFor(a => a.Note)
            .PaymentNote();

        RuleFor(a => a.Method)
            .PaymentMethod();

        RuleFor(a => a.Date)
            .PaymentDate();

    }

}