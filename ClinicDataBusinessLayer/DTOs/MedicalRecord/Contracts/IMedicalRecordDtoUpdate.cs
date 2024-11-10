namespace ClinicDataBusinessLayer.DTOs.MedicalRecord.Contracts;

public interface IMedicalRecordDtoUpdate : IMedicalRecordDtoBase, IDtoUpdate
{
    public int Id { get; set; }
}