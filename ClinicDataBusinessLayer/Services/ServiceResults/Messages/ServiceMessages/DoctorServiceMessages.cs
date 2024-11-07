using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Doctor;
using ClinicDataBusinessLayer.DTOs.Doctor.Contracts;
using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages
{
    public class DoctorServiceMessages : IServiceMessages, IScopedService, IScopedMessage
    {
        public IdErrorMessage NotFound(params object[] keyValues)
        {

            return new IdErrorMessage(
                IdErrorMessageStatus.NotFound,
                nameof(Resources.DoctorMessages.DoctorNotFound),
                Resources.DoctorMessages.DoctorNotFound,
                keyValues
            );
        }

        public IdErrorMessage NotDeleted(params object[] keyValues)
        {

            return new IdErrorMessage(
                IdErrorMessageStatus.BadData,
                nameof(Resources.DoctorMessages.NotDeleted),
                Resources.DoctorMessages.NotDeleted,
                keyValues
            );
        }
    }

}
