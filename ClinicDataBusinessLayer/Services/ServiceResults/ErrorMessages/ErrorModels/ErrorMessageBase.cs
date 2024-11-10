using static System.Text.RegularExpressions.Regex;

namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages.ErrorModels;

public abstract class ErrorMessageBase
{
    private readonly string _errorTitle;

    protected ErrorMessageBase(ErrorMessageBaseStatus status, string errorTitle)
    {
        _errorTitle = errorTitle;
        Status = status;
    }

    public ErrorMessageBaseStatus Status { get; private set; }
    public abstract string ErrorMessage { get; }
    public string ErrorTitle
    {

        get
        {
            var result = Replace(_errorTitle, "([a-z])([A-Z])", "$1 $2");

            result = Replace(result, @"\b[a-zA-Z]", match => match.Value.ToLower());

            return result;
        }
    }
}