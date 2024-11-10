namespace ClinicDataBusinessLayer.DTOs.Prescription;

public class PrescriptionDtoAdd : IPrescriptionDtoAdd
{
    public int MedicalRecordId { get; set; }
    public string? Note { get; set; } 
    public ICollection<MedicationDtoAddFromPrescription> Medications { get; set; } = new List<MedicationDtoAddFromPrescription>();
}