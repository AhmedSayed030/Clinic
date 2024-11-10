namespace ClinicDataBusinessLayer.DTOs.MedicalRecord;

public class MedicalRecordDtoUpdated : IMedicalRecordDtoUpdate
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public PrescriptionDtoUpdateFromMedicalRecord? Prescription { get; set; } = null!;
}