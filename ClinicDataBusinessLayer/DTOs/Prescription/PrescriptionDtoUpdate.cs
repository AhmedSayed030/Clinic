namespace ClinicDataBusinessLayer.DTOs.Prescription;

public class PrescriptionDtoUpdate : IPrescriptionDtoUpdate
{
    public int Id { get; set; }
    public string? Note { get; set; }
    public ICollection<MedicationDtoUpdateFromPrescription> Medications { get; set; } = new List<MedicationDtoUpdateFromPrescription>();
}