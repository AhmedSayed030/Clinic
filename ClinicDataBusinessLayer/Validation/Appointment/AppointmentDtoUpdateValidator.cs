namespace ClinicDataBusinessLayer.Validation.Appointment;

public class AppointmentDtoUpdateValidator : AbstractValidator<AppointmentDtoUpdate>
{
    public AppointmentDtoUpdateValidator()
    {

        RuleFor(a => a.Id)
            .AppointmentId();

        RuleFor(a => a.PatientId)
            .PatientId();
        
        RuleFor(a => a.DoctorId)
            .DoctorId();

        RuleFor(a => a.Status)
            .AppointmentStatus();

        RuleFor(a => a.Date)
            .AppointmentDate();
    }

}