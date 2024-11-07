using ClinicDataBusinessLayer.DTOs.Appointment;
using ClinicDataBusinessLayer.DTOs.MedicalRecord.Contracts;
using ClinicDataBusinessLayer.DTOs.Prescription;

namespace ClinicDataBusinessLayer.DTOs.MedicalRecord
{
    public partial class MedicalRecordDto : IMedicalRecordDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public PrescriptionDto? Prescription { get; set; } = null;
    }


}
