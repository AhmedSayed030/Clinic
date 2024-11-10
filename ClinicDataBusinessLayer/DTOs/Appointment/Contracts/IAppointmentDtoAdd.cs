namespace ClinicDataBusinessLayer.DTOs.Appointment.Contracts;

public interface IAppointmentDtoAdd : IAppointmentDtoBase, IDtoAdd
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
}