namespace ClinicDataBusinessLayer.Services.ServiceResults;

public interface IServiceResult
{
    public bool IsSuccess => Status == ServiceResultStatus.Success;
    public ServiceResultStatus Status { get; }
    public ErrorMessageBase? Error { get; }
}
public interface IServiceResult<out T> : IServiceResult
{
    public T? Data { get; }

}