using ClinicDataAccessLayer.Data;
using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Validation.Medication;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClinicDataBusinessLayer.Validation.Prescription
{
    public class PrescriptionDtoAddValidator : AbstractValidator<PrescriptionDtoAdd>
    {
        public PrescriptionDtoAddValidator()
        {
            RuleFor(p => p.MedicalRecordId)
               .GreaterThan(0)
               .WithMessage(Resources.MedicalRecordMessages.KeyGreaterThanZero);

            RuleFor(p => p.Note)
              .MaximumLength(500)
              .WithMessage(Resources.PrescriptionMessages.NoteMaxLength);

            RuleForEach(p => p.Medications)
               .SetValidator(p => new MedicationDtoAddFromPrescriptionValidator());

        }
    }

}
