namespace ClinicDataAccessLayer.Entities;

public class Prescription : IEntry
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public string? Note { get; set; }
    public ICollection<Medication> Medications { get; set; } = new List<Medication>();
    public MedicalRecord MedicalRecord { get; set; } = null!;
}