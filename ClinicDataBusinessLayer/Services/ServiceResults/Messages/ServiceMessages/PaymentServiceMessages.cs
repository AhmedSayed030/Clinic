using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Payment;
using ClinicDataBusinessLayer.DTOs.Payment.Contracts;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages
{
    public class PaymentServiceMessages : IServiceMessages, IScopedService, IScopedMessage
    {
        public IdErrorMessage NotFound(params object[] keyValues)
        {
            return new IdErrorMessage(
                IdErrorMessageStatus.NotFound,
                nameof(Resources.PaymentMessages.PaymentNotFound),
                Resources.PaymentMessages.PaymentNotFound,
                keyValues
            );
        }

    }

}
