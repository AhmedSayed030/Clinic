namespace ClinicDataBusinessLayer.Validation.MedicalRecord;

public class MedicalRecordDtoAddValidator : AbstractValidator<MedicalRecordDtoAdd>
{
    public MedicalRecordDtoAddValidator(IValidator<PrescriptionDtoAddFromMedicalRecord> prescriptionDtoAddFromMedicalRecordValidator)
    {
        RuleFor(d => d.AppointmentId)
            .AppointmentId();

        RuleFor(m => m.Diagnosis)
            .MedicalRecordDiagnosis();

        RuleFor(m => m.Description)
            .MedicalRecordDescription();

        RuleFor(m => m.Prescription!)
            .SetValidator(prescriptionDtoAddFromMedicalRecordValidator);
    }

}