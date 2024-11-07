using ClinicDataAccessLayer.Data;
using ClinicDataBusinessLayer.DTOs.MedicalRecord;
using ClinicDataBusinessLayer.Validation.Prescription;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ClinicDataBusinessLayer.Validation.MedicalRecord
{
    public class MedicalRecordDtoUpdateValidator : AbstractValidator<MedicalRecordDtoUpdated>
    {

        public MedicalRecordDtoUpdateValidator()
        {
            RuleFor(m => m.Id)
               .GreaterThan(0)
               .WithMessage(Resources.MedicalRecordMessages.KeyGreaterThanZero);


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
               .SetValidator(m => new PrescriptionDtoUpdateFromMedicalRecordValidator());
        }

    }


}
