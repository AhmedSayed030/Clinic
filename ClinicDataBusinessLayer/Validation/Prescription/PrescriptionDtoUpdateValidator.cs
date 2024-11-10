namespace ClinicDataBusinessLayer.Validation.Prescription;

public class PrescriptionDtoUpdateValidator : AbstractValidator<PrescriptionDtoUpdate>
{
    public PrescriptionDtoUpdateValidator(IValidator<MedicationDtoUpdateFromPrescription> validatorMedicationDtoUpdateFromPrescription)
    {
        RuleFor(p => p.Id)
            .PrescriptionId();

        RuleFor(p => p.Note)
            .PrescriptionNote();

        RuleForEach(p => p.Medications)
            .SetValidator(validatorMedicationDtoUpdateFromPrescription);
    }

}