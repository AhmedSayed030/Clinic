using ClinicDataBusinessLayer.DTOs.Appointment.Contracts;
using ClinicDataBusinessLayer.DTOs.Doctor.Contracts;

namespace ClinicDataBusinessLayer.DTOs.Appointment
{
    public class AppointmentDtoAdd : IAppointmentDtoAdd
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
    }
}
