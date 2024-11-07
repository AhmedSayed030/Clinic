using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;

public class ServerErrorMessage : ErrorMessageBase
{
    public Exception Exception { get; private set; }
    public override string ErrorMessage { get; }
    public ServerErrorMessage(Exception exception, string errorTitle, string error)
        : base(ErrorMessageStatus.ServerError, errorTitle)
    {
        Exception = exception;
        ErrorMessage = error;
    }
}

