namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class MedicalRecordServiceErrorMessages : IServiceErrorMessages, IScopedErrorMessages
{
    public IdError NotFound(params object[] keyValues)
    {
        return new IdError(
            IdErrorStatus.NotFound,
            nameof(Resources.MedicalRecordMessages.MedicalRecordNotFound),
            Resources.MedicalRecordMessages.MedicalRecordNotFound,
            keyValues
        );
    }
}