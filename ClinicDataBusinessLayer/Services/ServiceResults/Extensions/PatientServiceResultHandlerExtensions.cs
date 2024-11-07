using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Extensions
{
    public static class PatientServiceResultHandlerExtensions
    {
        public static IServiceResult NotDeleted(this ServiceResultHandler<PatientServiceMessages> serviceResultHandler,
            params object[] keyValues)
        {
            return ServiceResult.Failure(serviceResultHandler.ServerMessages.NotDeleted(keyValues));
        }

    }
}
