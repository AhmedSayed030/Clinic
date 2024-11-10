
namespace ClinicDataBusinessLayer.DTOs.Medication;

public class MedicationDtoUpdate : IMedicationDtoUpdate
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public byte Frequency { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}