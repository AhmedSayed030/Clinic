namespace ClinicDataBusinessLayer.DTOs.Patient;

public class PatientDtoUpdate : PersonDto, IPatientDtoUpdate
{
    public int Id { get; set; }
}