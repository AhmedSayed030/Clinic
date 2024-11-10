namespace ClinicDataAccessLayer.Entities;

public class MedicalRecord : IEntry
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;

    public Appointment Appointment { get; set; } = null!;
    public Prescription? Prescription { get; set; } = null;
}