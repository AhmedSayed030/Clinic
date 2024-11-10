namespace ClinicDataBusinessLayer.DTOs.Prescription.Contracts;

public interface IPrescriptionDtoUpdate : IPrescriptionDtoBase, IDtoUpdate
{
    public int Id { get; set; }
}