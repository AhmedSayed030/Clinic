using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.DTOs.Patient.Contracts;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages
{
    public class PatientServiceMessages : IServiceMessages, IScopedService, IScopedMessage
    {
        public IdErrorMessage NotFound(params object[] keyValues)
        {

            return new IdErrorMessage(
                IdErrorMessageStatus.NotFound,
                nameof(Resources.PatientMessages.PatientNotFound),
                Resources.PatientMessages.PatientNotFound,
                keyValues
            );
        }
        public IdErrorMessage NotDeleted(params object[] keyValues)
        {

            return new IdErrorMessage(
                IdErrorMessageStatus.BadData,
                nameof(Resources.PatientMessages.NotDeleted),
                Resources.PatientMessages.NotDeleted,
                keyValues
            );
        }

    }
}
