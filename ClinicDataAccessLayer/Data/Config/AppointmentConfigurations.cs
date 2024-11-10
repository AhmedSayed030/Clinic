namespace ClinicDataAccessLayer.Data.Config;

public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasOne(e => e.Patient)
            .WithMany(e => e.Appointments)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Doctor)
            .WithMany(e => e.Appointments)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}