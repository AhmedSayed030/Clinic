namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class AppointmentServiceErrorMessages : IServiceErrorMessages, IScopedErrorMessages
{
    public IdError NotFound(params object[] keyValues)
    {
        return new IdError(
            IdErrorStatus.NotFound,
            nameof(Resources.AppointmentMessages.AppointmentNotFound),
            Resources.AppointmentMessages.AppointmentNotFound,
            keyValues
        );
    }

}