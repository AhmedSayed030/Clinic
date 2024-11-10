namespace ClinicDataBusinessLayer.DTOs.Person;

public interface IPersonDto : IDto
{
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
}