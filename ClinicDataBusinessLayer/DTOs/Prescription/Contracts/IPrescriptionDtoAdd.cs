namespace ClinicDataBusinessLayer.DTOs.Prescription.Contracts;

public interface IPrescriptionDtoAdd : IPrescriptionDtoBase, IDtoAdd
{
    public int MedicalRecordId { get; set; }
    public string? Note { get; set; }
}