namespace ClinicDataAccessLayer.Entities;

public class Doctor : IEntry, IPerson, ISoftDeleteable
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string Specializatio { get; set; } = string.Empty;
    public Person Person { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime? DateDeleted { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}