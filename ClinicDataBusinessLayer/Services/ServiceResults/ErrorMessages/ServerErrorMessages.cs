namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages;

public class ServerErrorMessages : IScopedErrorMessages
{
    public ServerError ServerError(Exception exception)
    {
        return new ServerError(exception, "an error has occurred", "an unexpected error has occurred.");
    }
}