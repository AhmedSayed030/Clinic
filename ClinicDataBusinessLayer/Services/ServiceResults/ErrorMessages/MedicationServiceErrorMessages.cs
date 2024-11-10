namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class MedicationServiceErrorMessages : IServiceErrorMessages, IScopedErrorMessages
{
    public IdError NotFound(params object[] keyValues)
    {
        return new IdError(
            IdErrorStatus.NotFound,
            nameof(Resources.MedicationMessages.MedicationNotFound),
            Resources.MedicationMessages.MedicationNotFound,
            keyValues
        );
    }

}