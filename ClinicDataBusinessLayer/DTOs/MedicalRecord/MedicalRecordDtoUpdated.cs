
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Appointment;
using ClinicDataBusinessLayer.DTOs.MedicalRecord.Contracts;
using ClinicDataBusinessLayer.DTOs.Prescription;

namespace ClinicDataBusinessLayer.DTOs.MedicalRecord
{
    public partial class MedicalRecordDtoUpdated : IMedicalRecordDtoUpdate
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public PrescriptionDtoUpdateFromMedicalRecord? Prescription { get; set; } = null!;
    }

}
