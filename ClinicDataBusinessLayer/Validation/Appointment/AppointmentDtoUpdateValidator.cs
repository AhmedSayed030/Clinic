using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Appointment;
using FluentValidation;
using ClinicDataAccessLayer.Entities.Enums;

namespace ClinicDataBusinessLayer.Validation.Appointment
{
    public class AppointmentDtoUpdateValidator : AbstractValidator<AppointmentDtoUpdate>
    {
        public AppointmentDtoUpdateValidator()
        {

            RuleFor(a => a.Id)
             .GreaterThan(0)
             .WithMessage(Resources.AppointmentMessages.KeyGreaterThanZero);

            RuleFor(a => a.PatientId)
              .GreaterThan(0)
              .WithMessage(Resources.PatientMessages.KeyGreaterThanZero);

            RuleFor(a => a.DoctorId)
              .GreaterThan(0)
              .WithMessage(Resources.DoctorMessages.KeyGreaterThanZero);

            RuleFor(a => a.Status)
                .NotEmpty()
                .WithMessage(Resources.AppointmentMessages.StatusRequired)
                .IsEnumName(typeof(AppointmentStatus), false)
                .WithMessage(Resources.AppointmentMessages.StatusInvalid);

            RuleFor(a => a.Date)
                .NotEmpty()
                .WithMessage(Resources.AppointmentMessages.DateRequired)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage(Resources.AppointmentMessages.DateInvalid);
        }

    }


}
