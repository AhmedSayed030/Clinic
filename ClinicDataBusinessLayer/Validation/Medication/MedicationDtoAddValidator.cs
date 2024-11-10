namespace ClinicDataBusinessLayer.Validation.Medication;

public class MedicationDtoAddValidator : AbstractValidator<MedicationDtoAdd>
{
    public MedicationDtoAddValidator()
    {

        RuleFor(m => m.PrescriptionId)
            .PrescriptionId();

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