namespace ClinicDataBusinessLayer.DTOs.Prescription;

public class PrescriptionDto : IPrescriptionDto
{

    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public string? Note { get; set; }
    public ICollection<MedicationDto> Medications { get; set; } = new List<MedicationDto>();
}