using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;

namespace ClinicDataBusinessLayer.Services.ServiceResults
{
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
    public class ServiceResultHandler<TServerMessages> : ServiceResultHandler
        where TServerMessages : class, IServiceMessages
    {
        public TServerMessages ServerMessages { get; private set; }

        public ServiceResultHandler(TServerMessages serverErrors)
        {
            ServerMessages = serverErrors;
        }
        public IServiceResult NotFound(params object[] keyValues)
        {
            return ServiceResult.Failure(ServerMessages.NotFound(keyValues));
        }
        public IServiceResult<TResult> NotFound<TResult>(params object[] keyValues)
        {
            return ServiceResult<TResult>.Failure(ServerMessages.NotFound(keyValues));
        }
    }
}
