namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class PatientServiceErrorMessages : IServiceErrorMessages, IScopedErrorMessages
{
    public IdError NotFound(params object[] keyValues)
    {

        return new IdError(
            IdErrorStatus.NotFound,
            nameof(Resources.PatientMessages.PatientNotFound),
            Resources.PatientMessages.PatientNotFound,
            keyValues
        );
    }
    public IdError NotDeleted(params object[] keyValues)
    {

        return new IdError(
            IdErrorStatus.BadData,
            nameof(Resources.PatientMessages.NotDeleted),
            Resources.PatientMessages.NotDeleted,
            keyValues
        );
    }

}