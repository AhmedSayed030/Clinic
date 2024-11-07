using ClinicDataBusinessLayer.DTOs.Appointment;
using ClinicDataBusinessLayer.DTOs.Payment.Contracts;

namespace ClinicDataBusinessLayer.DTOs.Payment
{
    public class PaymentDtoUpdaet : IPaymentDtoUpdate
    {
        public int Id { get; set; }
        public string Method { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
