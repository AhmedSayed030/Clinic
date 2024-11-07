using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Validation.Medication;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Prescription
{
    public class PrescriptionDtoUpdateFromMedicalRecordValidator : AbstractValidator<PrescriptionDtoUpdateFromMedicalRecord>
    {
        public PrescriptionDtoUpdateFromMedicalRecordValidator()
        {
           
            RuleFor(p => p.Note)
              .MaximumLength(500)
              .WithMessage(Resources.PrescriptionMessages.NoteMaxLength);

            RuleForEach(p => p.Medications)
               .SetValidator(p => new MedicationDtoUpdateFromPrescriptionValidator());
        }
    }

}
