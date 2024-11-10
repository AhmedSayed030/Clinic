namespace ClinicDataBusinessLayer.DTOs.Patient.Contracts;

public interface IPatientDtoUpdate : IPatientDtoBase, IDtoUpdate
{
    public int Id { get; set; }
}