namespace ClinicDataBusinessLayer.DTOs.Appointment.Contracts;

public interface IAppointmentDtoUpdate : IAppointmentDtoBase, IDtoUpdate
{
    public int Id { get; set; }
}