
namespace ClinicDataBusinessLayer.DTOs.Prescription;

public class PrescriptionDtoAddFromMedicalRecord : IPrescriptionDtoBase, IDtoAdd
{
    public string? Note { get; set; }
    public ICollection<MedicationDtoAddFromPrescription> Medications { get; set; } = new List<MedicationDtoAddFromPrescription>();

}