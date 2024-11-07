using ClinicDataBusinessLayer.DTOs.Payment.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDataBusinessLayer.DTOs.Payment
{
    public class PaymentDtoAdd : IPaymentDtoAdd
    {
        public int AppointmentId { get; set; }
        public string Method { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
