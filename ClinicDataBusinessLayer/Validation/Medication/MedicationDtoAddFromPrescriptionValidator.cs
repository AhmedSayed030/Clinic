namespace ClinicDataBusinessLayer.Validation.Medication;

public class MedicationDtoAddFromPrescriptionValidator : AbstractValidator<MedicationDtoAddFromPrescription>
{
    public MedicationDtoAddFromPrescriptionValidator()
    {

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