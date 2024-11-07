using ClinicDataAccessLayer.Data;
using ClinicDataBusinessLayer.DTOs.Prescription;

using ClinicDataBusinessLayer.Validation.Medication;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClinicDataBusinessLayer.Validation.Prescription
{
    public class PrescriptionDtoUpdateValidator : AbstractValidator<PrescriptionDtoUpdate>
    {
        public PrescriptionDtoUpdateValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage(Resources.PrescriptionMessages.KeyGreaterThanZero);

            RuleFor(p => p.Note)
                .MaximumLength(500)
                .WithMessage(Resources.PrescriptionMessages.NoteMaxLength);

            RuleForEach(p => p.Medications)
               .SetValidator(p => new MedicationDtoUpdateFromPrescriptionValidator());
        }

    }
}
