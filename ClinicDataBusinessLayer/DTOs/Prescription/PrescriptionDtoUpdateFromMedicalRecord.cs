using ClinicDataBusinessLayer.DTOs.Medication;

namespace ClinicDataBusinessLayer.DTOs.Prescription
{
    public partial class PrescriptionDtoUpdateFromMedicalRecord
    {
        public string? Note { get; set; }
        public ICollection<MedicationDtoUpdateFromPrescription> Medications { get; set; } = new List<MedicationDtoUpdateFromPrescription>();
    }

}
