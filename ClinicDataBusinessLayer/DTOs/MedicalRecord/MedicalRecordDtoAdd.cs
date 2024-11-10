namespace ClinicDataBusinessLayer.DTOs.MedicalRecord;

public class MedicalRecordDtoAdd : IMedicalRecordDtoAdd
{
    public int AppointmentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public PrescriptionDtoAddFromMedicalRecord? Prescription { get; set; } = null!;

}