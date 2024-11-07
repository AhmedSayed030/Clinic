using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages
{
    public class MedicalRecordServiceMessages : IServiceMessages, IScopedService, IScopedMessage
    {
        public IdErrorMessage NotFound(params object[] keyValues)
        {
            return new IdErrorMessage(
                IdErrorMessageStatus.NotFound,
                nameof(Resources.MedicalRecordMessages.MedicalRecordNotFound),
                Resources.MedicalRecordMessages.MedicalRecordNotFound,
                keyValues
            );
        }
    }

}
