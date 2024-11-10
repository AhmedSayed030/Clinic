namespace ClinicDataAccessLayer.Data.Config;

public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasOne(e => e.Appointment)
            .WithOne();

        builder.Property(e => e.Note)
            .HasMaxLength(500);

        builder.Property(e => e.Method)
            .HasMaxLength(100);
    }
}