using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Medication;
using ClinicDataBusinessLayer.DTOs.Medication.Contracts;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages
{
    public class MedicationServiceMessages : IServiceMessages, IScopedService, IScopedMessage
    {
        public IdErrorMessage NotFound(params object[] keyValues)
        {
            return new IdErrorMessage(
                IdErrorMessageStatus.NotFound,
                nameof(Resources.MedicationMessages.MedicationNotFound),
                Resources.MedicationMessages.MedicationNotFound,
                keyValues
            );
        }

    }

}
