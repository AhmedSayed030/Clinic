using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
public class IdErrorMessage : ErrorMessageBase
{
    private readonly object[] _args;
    private readonly string _originalError;

    public override string ErrorMessage
    {
        get
        {
            return _originalError.Replace("{PropertyValue}", string.Join(", ", _args));
        }
    }

    public IdErrorMessage(IdErrorMessageStatus status, string errorTitle, string originalError, params object[] args)
        : base((ErrorMessageStatus)status, errorTitle)
    {
        _originalError = originalError;
        _args = args;
    }
}

