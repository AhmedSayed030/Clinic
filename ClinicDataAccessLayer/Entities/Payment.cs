namespace ClinicDataAccessLayer.Entities;

public class Payment : IEntry
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public string Method { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public DateTime Date { get; set; }
    public Appointment Appointment { get; set; } = null!;
}