namespace ClinicDataBusinessLayer.Validation.Medication;

public class MedicationDtoUpdateFromPrescriptionValidator : AbstractValidator<MedicationDtoUpdateFromPrescription>
{
    public MedicationDtoUpdateFromPrescriptionValidator()
    {
        RuleFor(m => m.Id)
            .MedicationId();

        RuleFor(m => m.Frequency)
            .MedicationFrequency();

        RuleFor(m => m.Name)
            .MedicationName();

        RuleFor(m => m.Dosage)
            .MedicationDosage();

        RuleFor(m => m.StartDate)
            .MedicationStartDate();

        RuleFor(m => m.EndDate)
            .MedicationEndDate();

    }

}