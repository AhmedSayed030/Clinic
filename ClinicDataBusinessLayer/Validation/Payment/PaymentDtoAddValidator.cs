namespace ClinicDataBusinessLayer.Validation.Payment;

public class PaymentDtoAddValidator : AbstractValidator<PaymentDtoAdd>
{
    public PaymentDtoAddValidator()
    {
        RuleFor(a => a.AppointmentId)
            .AppointmentId();

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