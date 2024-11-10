namespace ClinicDataBusinessLayer.Validation.Prescription;

public class PrescriptionDtoUpdateFromMedicalRecordValidator : AbstractValidator<PrescriptionDtoUpdateFromMedicalRecord>
{
    public PrescriptionDtoUpdateFromMedicalRecordValidator(IValidator<MedicationDtoUpdateFromPrescription> validatorMedicationDtoUpdateFromPrescription)
    {

        RuleFor(p => p.Note)
            .PrescriptionNote();

        RuleForEach(p => p.Medications)
            .SetValidator(validatorMedicationDtoUpdateFromPrescription);
    }
}