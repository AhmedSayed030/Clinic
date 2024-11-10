namespace ClinicDataBusinessLayer.Services.ServiceResults.Extensions;

public static class PatientServiceResultHandlerExtensions
{
    public static IServiceResult NotDeleted(this ServiceResultHandler<PatientServiceErrorMessages> serviceResultHandler,
        params object[] keyValues)
    {
        return ServiceResult.Failure(serviceResultHandler.ServerErrorMessages.NotDeleted(keyValues));
    }

}