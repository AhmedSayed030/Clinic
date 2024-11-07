using ClinicDataAccessLayer.Data;
using ClinicDataBusinessLayer.DTOs.Appointment;
using FluentValidation;

namespace ClinicDataBusinessLayer.Validation.Appointment
{
    public class AppointmentDtoAddValidator : AbstractValidator<AppointmentDtoAdd>
    {
        public AppointmentDtoAddValidator()
        {

            RuleFor(a => a.PatientId)
               .GreaterThan(0)
               .WithMessage(Resources.PatientMessages.KeyGreaterThanZero);

            RuleFor(a => a.DoctorId)
              .GreaterThan(0)
              .WithMessage(Resources.DoctorMessages.KeyGreaterThanZero);

            RuleFor(a => a.Date)
              .NotEmpty()
              .WithMessage(Resources.AppointmentMessages.DateRequired)
              .GreaterThanOrEqualTo(DateTime.Now)
              .WithMessage(Resources.AppointmentMessages.DateInvalid);
        }

    }

}
