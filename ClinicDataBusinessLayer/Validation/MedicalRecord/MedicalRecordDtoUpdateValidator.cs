namespace ClinicDataBusinessLayer.Validation.MedicalRecord;

public class MedicalRecordDtoUpdateValidator : AbstractValidator<MedicalRecordDtoUpdated>
{
    public MedicalRecordDtoUpdateValidator(IValidator<PrescriptionDtoUpdateFromMedicalRecord> prescriptionDtoAddFromMedicalRecordValidator)
    {
        RuleFor(m => m.Id)
            .MedicalRecordId();

        RuleFor(m => m.Diagnosis)
            .MedicalRecordDiagnosis();

        RuleFor(m => m.Description)
            .MedicalRecordDiagnosis();

        RuleFor(m => m.Prescription!)
            .SetValidator(prescriptionDtoAddFromMedicalRecordValidator);
    }

}