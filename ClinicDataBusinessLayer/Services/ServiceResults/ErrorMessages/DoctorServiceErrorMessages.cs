namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class DoctorServiceErrorMessages : IServiceErrorMessages, IScopedErrorMessages
{
    public IdError NotFound(params object[] keyValues)
    {

        return new IdError(
            IdErrorStatus.NotFound,
            nameof(Resources.DoctorMessages.DoctorNotFound),
            Resources.DoctorMessages.DoctorNotFound,
            keyValues
        );
    }

    public IdError NotDeleted(params object[] keyValues)
    {
        return new IdError(
            IdErrorStatus.BadData,
            nameof(Resources.DoctorMessages.NotDeleted),
            Resources.DoctorMessages.NotDeleted,
            keyValues
        );
    }
}