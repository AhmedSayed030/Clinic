using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ErrorMessages.Status;
using System.Text.RegularExpressions;

public abstract class ErrorMessageBase
{
    public ErrorMessageStatus Status { get; private set; }

    private string _errorTitle;
    public string ErrorTitle {

        get
        {
            string result = Regex.Replace(_errorTitle, "([a-z])([A-Z])", "$1 $2");

            result = Regex.Replace(result, @"\b[a-zA-Z]", match => match.Value.ToLower());

            return result;
        }
        private set 
        {
            _errorTitle = value;
        } 
    }
    public abstract string ErrorMessage { get; }
    protected ErrorMessageBase(ErrorMessageStatus status, string errorTitle)
    {
        Status = status;
        _errorTitle = errorTitle;
    }

}
