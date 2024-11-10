namespace ClinicDataBusinessLayer.Services.ServiceResults.Extensions;

public static class DoctorServiceResultExtensions
{
    public static IServiceResult NotDeleted(this ServiceResultHandler<DoctorServiceErrorMessages> serviceResultHandler,
        params object[] keyValues)
    {
        return ServiceResult.Failure(serviceResultHandler.ServerErrorMessages.NotDeleted(keyValues));
    }
}