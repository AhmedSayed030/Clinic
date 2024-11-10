namespace ClinicDataBusinessLayer.Services.ServiceResults;
internal class ServiceResult : IServiceResult
{
    public ServiceResultStatus Status { get; private set; }
    public ErrorMessageBase? Error { get; private set; }
    protected ServiceResult()
    {
        Status = ServiceResultStatus.Success;
    }
    protected ServiceResult(ErrorMessageBase errorMessage)
    {
        Status = (ServiceResultStatus)errorMessage.Status;
        Error = errorMessage;
    }

    public static IServiceResult Success()
    {
        return new ServiceResult();
    }

    public static IServiceResult Failure(ErrorMessageBase errorMessage)
    {
        return new ServiceResult(errorMessage);
    }
}
internal class ServiceResult<TResult> : ServiceResult, IServiceResult<TResult>
{
    public TResult? Data { get; private set; }
    protected ServiceResult(TResult data)
    {
        Data = data;
    }
    protected ServiceResult(ErrorMessageBase errorMessage) : base(errorMessage)
    {
    }

    public static IServiceResult<TResult> Success(TResult data)
    {
        return new ServiceResult<TResult>(data);
    }
    public new static IServiceResult<TResult> Failure(ErrorMessageBase errorMessage)
    {
        return new ServiceResult<TResult>(errorMessage);
    }
}