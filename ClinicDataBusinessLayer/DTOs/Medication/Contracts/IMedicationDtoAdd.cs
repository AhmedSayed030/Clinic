namespace ClinicDataBusinessLayer.DTOs.Medication.Contracts;

public interface IMedicationDtoAdd : IMedicationDtoBase, IDtoAdd
{
    public int PrescriptionId { get; set; }
    public string Name { get; set; }
    public string Dosage { get; set; }
    public byte Frequency { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}