namespace ClinicDataBusinessLayer.DTOs.Appointment;

public class AppointmentDtoUpdate : IAppointmentDtoUpdate
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
}