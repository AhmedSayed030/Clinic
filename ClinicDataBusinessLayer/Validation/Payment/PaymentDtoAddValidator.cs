using ClinicDataBusinessLayer.DTOs.Payment;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Payment
{
    public class PaymentDtoAddValidator : AbstractValidator<PaymentDtoAdd>
    {
        public PaymentDtoAddValidator()
        {
            RuleFor(a => a.AppointmentId)
                .GreaterThan(0)
                .WithMessage(Resources.PaymentMessages.AppointmentIdGreaterThan);

            RuleFor(a => a.Amount)
                .GreaterThan(-1)
                .WithMessage(Resources.PaymentMessages.AmountGreaterThanOrEqualTo);

            RuleFor(a => a.Note)
                .MaximumLength(500)
                .WithMessage(Resources.PaymentMessages.NoteMaximumLength);

            RuleFor(a => a.Method)
                .MaximumLength(100)
                .WithMessage(Resources.PaymentMessages.MethodMaximumLength);

            RuleFor(a => a.Date)
                .NotEmpty()
                .WithMessage(Resources.PaymentMessages.DateNotEmpty)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage(Resources.PaymentMessages.DateLessThanOrEqualToNow);
        }


    }

}
