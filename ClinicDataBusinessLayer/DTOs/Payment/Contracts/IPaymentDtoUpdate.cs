using ClinicDataBusinessLayer.DTOs.Contracts;
using ClinicDataBusinessLayer.DTOs.Patient.Contracts;

namespace ClinicDataBusinessLayer.DTOs.Payment.Contracts
{
    public interface IPaymentDtoUpdate : IPaymentDtoBase, IDtoUpdate
    {
        public int Id { get; set; }
    }
}
