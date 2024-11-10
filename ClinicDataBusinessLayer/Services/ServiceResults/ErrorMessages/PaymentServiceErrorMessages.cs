namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class PaymentServiceErrorMessages : IServiceErrorMessages, IScopedErrorMessages
{
    public IdError NotFound(params object[] keyValues)
    {
        return new IdError(
            IdErrorStatus.NotFound,
            nameof(Resources.PaymentMessages.PaymentNotFound),
            Resources.PaymentMessages.PaymentNotFound,
            keyValues
        );
    }

}