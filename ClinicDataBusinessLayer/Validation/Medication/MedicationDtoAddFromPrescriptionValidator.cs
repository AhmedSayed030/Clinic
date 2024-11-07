using ClinicDataBusinessLayer.DTOs.Medication;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Medication
{
    public class MedicationDtoAddFromPrescriptionValidator : AbstractValidator<MedicationDtoAddFromPrescription>
    {
        public MedicationDtoAddFromPrescriptionValidator()
        {
           
            RuleFor(m => m.Frequency)
               .GreaterThan((byte)0)
               .WithMessage(Resources.MedicationMessages.FrequencyGreaterThanZero);

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage(Resources.MedicationMessages.NameNotEmpty)
                .MaximumLength(100)
                .WithMessage(Resources.MedicationMessages.NameMaxLength);

            RuleFor(m => m.Dosage)
                .NotEmpty()
                .WithMessage(Resources.MedicationMessages.DosageNotEmpty)
                .MaximumLength(100)
                .WithMessage(Resources.MedicationMessages.DosageMaxLength);

            RuleFor(m => m.StartDate)
                .NotEmpty()
                .WithMessage(Resources.MedicationMessages.StartDateInFuture)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage(Resources.MedicationMessages.StartDateInFuture);

            RuleFor(m => m.EndDate)
                .NotEmpty()
                .WithMessage(Resources.MedicationMessages.EndDateInFuture)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage(Resources.MedicationMessages.EndDateInFuture);

        }

    }

}
