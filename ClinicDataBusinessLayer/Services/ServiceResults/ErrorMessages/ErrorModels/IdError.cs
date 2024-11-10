namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages.ErrorModels;

public class IdError : ErrorMessageBase
{
    private readonly string _originalError;
    private readonly object[] _args;
    public override string ErrorMessage => _originalError.Replace("{PropertyValue}", string.Join(", ", _args));
    public IdError(IdErrorStatus status, string errorTitle, string originalError, params object[] args) : base((ErrorMessageBaseStatus)status, errorTitle)
    {
        _originalError = originalError;
        _args = args;
    }

}