namespace ClinicDataBusinessLayer.DTOs.Patient;

public class PatientDto : PersonDto, IPatientDto
{
    [JsonProperty(Order = -1)]
    public int Id { get; set; }
}