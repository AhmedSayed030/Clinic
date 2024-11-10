namespace ClinicDataAccessLayer.Data.Config;

public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");

        builder.HasOne(e => e.Person)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.Specializatio)
            .HasMaxLength(100);

    }

}