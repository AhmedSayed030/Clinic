namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class PrescriptionServiceErrorMessages : IServiceErrorMessages, IScopedErrorMessages
{
    public IdError NotFound(params object[] keyValues)
    {
        return new IdError(
            IdErrorStatus.NotFound,
            nameof(Resources.PrescriptionMessages.PrescriptionNotFound),
            Resources.PrescriptionMessages.PrescriptionNotFound,
            keyValues
        );
    }

}