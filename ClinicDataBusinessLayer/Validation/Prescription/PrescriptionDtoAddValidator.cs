namespace ClinicDataBusinessLayer.Validation.Prescription;

public class PrescriptionDtoAddValidator : AbstractValidator<PrescriptionDtoAdd>
{
    public PrescriptionDtoAddValidator(IValidator<MedicationDtoAddFromPrescription> validatorMedicationDtoAddFromPrescription)
    {
        RuleFor(p => p.MedicalRecordId)
            .MedicalRecordId();

        RuleFor(p => p.Note)
            .PrescriptionNote();

        RuleForEach(p => p.Medications)
            .SetValidator(validatorMedicationDtoAddFromPrescription);

    }
}