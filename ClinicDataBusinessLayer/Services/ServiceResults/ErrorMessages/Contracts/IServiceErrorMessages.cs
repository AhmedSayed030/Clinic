namespace ClinicDataBusinessLayer.Services.ServiceResults.ErrorMessages.Contracts;

public interface IServiceErrorMessages
{
    public IdError NotFound(params object[] keyValues);
}