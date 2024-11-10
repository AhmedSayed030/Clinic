namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages.ErrorModels;

public class ServerError : ErrorMessageBase
{
    public override string ErrorMessage { get; }
    public Exception Exception { get; private set; }
    public ServerError(Exception exception, string errorTitle, string error) : base(ErrorMessageBaseStatus.ServerError, errorTitle)
    {
        ErrorMessage = error;
        Exception = exception;
    }
}