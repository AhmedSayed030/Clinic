namespace ClinicDataBusinessLayer.Validation.Prescription;

public class PrescriptionDtoAddFromMedicalRecordValidator : AbstractValidator<PrescriptionDtoAddFromMedicalRecord>
{
    public PrescriptionDtoAddFromMedicalRecordValidator(IValidator<MedicationDtoAddFromPrescription> validatorMedicationDtoAddFromPrescription)
    {

        RuleFor(p => p.Note)
            .PrescriptionNote();

        RuleForEach(p => p.Medications)
            .SetValidator(validatorMedicationDtoAddFromPrescription);
           
    }
}