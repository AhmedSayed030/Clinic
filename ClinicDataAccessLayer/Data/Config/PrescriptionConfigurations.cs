namespace ClinicDataAccessLayer.Data.Config;

public class PrescriptionConfigurations : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescriptions");

        builder.Property(e => e.Note)
            .HasMaxLength(500);
    }
}