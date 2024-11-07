using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Patient.Contracts;
using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages
{
    public class PrescriptionServiceMessages : IServiceMessages, IScopedService, IScopedMessage
    {
        public IdErrorMessage NotFound(params object[] keyValues)
        {
            return new IdErrorMessage(
                IdErrorMessageStatus.NotFound,
                nameof(Resources.PrescriptionMessages.PrescriptionNotFound),
                Resources.PrescriptionMessages.PrescriptionNotFound,
                keyValues
            );
        }

    }

}
