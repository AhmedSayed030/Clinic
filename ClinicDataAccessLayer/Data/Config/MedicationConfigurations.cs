namespace ClinicDataAccessLayer.Data.Config;

public class MedicationConfigurations : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.ToTable("Medications");

        builder.HasOne(e => e.Prescription)
            .WithMany(e => e.Medications)
            .HasForeignKey(e => e.PrescriptionId);

        builder.Property(e => e.Name)
            .HasMaxLength(100);

        builder.Property(e => e.Dosage)
            .HasMaxLength(100);
    }
}