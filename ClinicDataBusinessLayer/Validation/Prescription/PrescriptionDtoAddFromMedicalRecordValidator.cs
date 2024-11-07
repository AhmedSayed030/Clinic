using ClinicDataBusinessLayer.DTOs.Medication;
using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Validation.Medication;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Prescription
{
    public class PrescriptionDtoAddFromMedicalRecordValidator : AbstractValidator<PrescriptionDtoAddFromMedicalRecord>
    {
        public PrescriptionDtoAddFromMedicalRecordValidator()
        {

            RuleFor(p => p.Note)
              .MaximumLength(500)
              .WithMessage(Resources.PrescriptionMessages.NoteMaxLength);

            RuleForEach(p => p.Medications)
                .SetValidator(p => new MedicationDtoAddFromPrescriptionValidator());
           
        }
    }

}
