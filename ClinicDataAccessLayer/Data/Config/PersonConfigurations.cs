namespace ClinicDataAccessLayer.Data.Config;

public class PersonConfigurations : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");

        builder.Property(e => e.Address)
            .HasMaxLength(500);

        builder.Property(e => e.Name)
            .HasMaxLength(25);

        builder.Property(e => e.Phone)
            .HasMaxLength(11);

        builder.Property(e => e.Email)
            .HasMaxLength(25);

    }

}