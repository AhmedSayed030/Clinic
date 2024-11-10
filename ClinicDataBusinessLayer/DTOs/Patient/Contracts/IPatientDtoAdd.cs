namespace ClinicDataBusinessLayer.DTOs.Patient.Contracts;

public interface IPatientDtoAdd : IPatientDtoBase, IDtoAdd
{
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
}