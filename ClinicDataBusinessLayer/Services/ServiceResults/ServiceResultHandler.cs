namespace ClinicDataBusinessLayer.Services.ServiceResults;

public class ServiceResultHandler
{
    public IServiceResult Success()
    {
        return ServiceResult.Success();
    }
    public IServiceResult<TResult> Success<TResult>(TResult result)
        where TResult : class
    {
        return ServiceResult<TResult>.Success(result);
    }
}
public class ServiceResultHandler<TServiceErrorMessages> : ServiceResultHandler
    where TServiceErrorMessages : class, IServiceErrorMessages
{
    public ServiceResultHandler(TServiceErrorMessages serviceErrorMessages)
    {
        ServerErrorMessages = serviceErrorMessages;
    }

    public TServiceErrorMessages ServerErrorMessages { get; }

    public IServiceResult NotFound(params object[] keyValues)
    {
        return ServiceResult.Failure(ServerErrorMessages.NotFound(keyValues));
    }
    public IServiceResult<TResult> NotFound<TResult>(params object[] keyValues)
    {
        return ServiceResult<TResult>.Failure(ServerErrorMessages.NotFound(keyValues));
    }
}