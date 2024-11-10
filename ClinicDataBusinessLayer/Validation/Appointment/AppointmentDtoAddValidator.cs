namespace ClinicDataBusinessLayer.Validation.Appointment;

public class AppointmentDtoAddValidator : AbstractValidator<AppointmentDtoAdd>
{
    public AppointmentDtoAddValidator()
    {

        RuleFor(a => a.PatientId)
            .PatientId();

        RuleFor(a => a.DoctorId)
            .DoctorId();

        RuleFor(a => a.Date)
            .AppointmentDate();
    }

}