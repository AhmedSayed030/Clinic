namespace ClinicDataAccessLayer.Data.Config;

public class PatientConfigurations : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasOne(e => e.Person)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

    }
}