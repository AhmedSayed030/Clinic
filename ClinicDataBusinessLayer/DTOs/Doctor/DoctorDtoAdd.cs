namespace ClinicDataBusinessLayer.DTOs.Doctor;

public class DoctorDtoAdd : PersonDto, IDoctorDtoAdd 
{
    public string Specializatio { get; set; } = string.Empty;
}