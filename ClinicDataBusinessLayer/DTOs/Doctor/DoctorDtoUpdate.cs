namespace ClinicDataBusinessLayer.DTOs.Doctor;

public class DoctorDtoUpdate : PersonDto, IDoctorDtoUpdate
{
    public int Id { get; set; }
    public string Specializatio { get; set; } = string.Empty;
}