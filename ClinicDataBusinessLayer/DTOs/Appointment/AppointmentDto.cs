namespace ClinicDataBusinessLayer.DTOs.Appointment;

public class AppointmentDto : IAppointmentDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public PatientDto Patient { get; set; } = null!;
    public DoctorDto Doctor { get; set; } = null!;
}