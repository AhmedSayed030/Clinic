namespace ClinicDataBusinessLayer.DTOs.Doctor.Contracts;

public interface IDoctorDtoUpdate : IDoctorDtoBase, IDtoUpdate
{
    public int Id { get; set; } 
}