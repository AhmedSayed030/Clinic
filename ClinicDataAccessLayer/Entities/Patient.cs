namespace ClinicDataAccessLayer.Entities;

public class Patient : IEntry, IPerson, ISoftDeleteable
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime? DateDeleted { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}