namespace ClinicDataBusinessLayer.DTOs.Prescription;

public class PrescriptionDtoUpdateFromMedicalRecord : IPrescriptionDtoBase, IDtoUpdate
{
    public string? Note { get; set; }
    public ICollection<MedicationDtoUpdateFromPrescription> Medications { get; set; } = new List<MedicationDtoUpdateFromPrescription>();
}