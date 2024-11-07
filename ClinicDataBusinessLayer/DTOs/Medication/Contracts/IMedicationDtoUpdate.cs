using ClinicDataBusinessLayer.DTOs.Contracts;

namespace ClinicDataBusinessLayer.DTOs.Medication.Contracts
{
    public interface IMedicationDtoUpdate : IMedicationDtoBase, IDtoUpdate
    {
        public int Id {  get; set; }
    }
}
