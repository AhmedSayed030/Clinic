namespace ClinicDataAccessLayer.Data.Config;

public class MedicalRecordConfigurations : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("MedicalRecords");

        builder.HasOne(e => e.Appointment)
            .WithOne();

        builder.HasOne(e => e.Prescription)
            .WithOne(e => e.MedicalRecord);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.Diagnosis)
            .HasMaxLength(250);
    }
}