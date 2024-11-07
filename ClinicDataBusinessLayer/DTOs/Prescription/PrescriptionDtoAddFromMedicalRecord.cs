
using ClinicDataBusinessLayer.DTOs.Medication;
using ClinicDataBusinessLayer.DTOs.Patient.Contracts;

namespace ClinicDataBusinessLayer.DTOs.Prescription
{
    public partial class PrescriptionDtoAddFromMedicalRecord
    {
        public string? Note { get; set; }
        public ICollection<MedicationDtoAddFromPrescription> Medications { get; set; } = new List<MedicationDtoAddFromPrescription>();

    }

}
