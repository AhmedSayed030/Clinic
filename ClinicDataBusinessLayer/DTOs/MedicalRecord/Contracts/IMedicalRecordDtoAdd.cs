namespace ClinicDataBusinessLayer.DTOs.MedicalRecord.Contracts;

public interface IMedicalRecordDtoAdd : IMedicalRecordDtoBase, IDtoAdd
{
    public int AppointmentId { get; set; }
    public string Description { get; set; }
    public string Diagnosis { get; set; }
}