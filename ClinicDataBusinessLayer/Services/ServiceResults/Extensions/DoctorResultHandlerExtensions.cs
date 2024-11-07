using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Extensions
{
    public static class DoctorResultHandlerExtensions
    {
        public static IServiceResult NotDeleted(this ServiceResultHandler<DoctorServiceMessages> serviceResultHandler,
            params object[] keyValues)
        {
            return ServiceResult.Failure(serviceResultHandler.ServerMessages.NotDeleted(keyValues));
        }
    }
}
