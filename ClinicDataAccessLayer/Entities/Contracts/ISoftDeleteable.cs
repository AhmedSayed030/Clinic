namespace ClinicDataAccessLayer.Entities.Contracts;

public interface ISoftDeleteable
{
    public bool IsDeleted { get; set; }
    public DateTime? DateDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DateDeleted = DateTime.Now;
    }

    public void UndoDelete()
    {
        IsDeleted = false;
        DateDeleted = null;
    }
}