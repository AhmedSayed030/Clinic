namespace ClinicDataBusinessLayer.DTOs.Appointment;

public class AppointmentDtoAdd : IAppointmentDtoAdd
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
}