using ClinicDataAccessLayer.Data;
using ClinicDataBusinessLayer.DTOs.MedicalRecord;
using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Validation.Prescription;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicDataBusinessLayer.Validation.MedicalRecord
{
    public class MedicalRecordDtoAddValidator : AbstractValidator<MedicalRecordDtoAdd>
    {
        public MedicalRecordDtoAddValidator()
        {
            RuleFor(d => d.AppointmentId)
             .GreaterThan(0)
             .WithMessage(Resources.AppointmentMessages.KeyGreaterThanZero);

            RuleFor(m => m.Diagnosis)
             .NotEmpty()
             .WithMessage(Resources.MedicalRecordMessages.DiagnosisNotEmpty)
             .MaximumLength(100)
             .WithMessage(Resources.MedicalRecordMessages.DiagnosisMaxLength);

            RuleFor(m => m.Description)
              .NotEmpty()
              .WithMessage(Resources.MedicalRecordMessages.DescriptionNotEmpty)
              .MaximumLength(500)
              .WithMessage(Resources.MedicalRecordMessages.DiagnosisMaxLength);

            RuleFor(m => m.Prescription!)
                .SetValidator(m => new PrescriptionDtoAddFromMedicalRecordValidator());
        }

    }


}
