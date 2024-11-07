using FluentValidation.Results;

namespace ClinicDataBusinessLayer.Services.ServiceResults
{
    internal class ServiceResult : IServiceResult
    {
        public ServiceResultStatus Status { get; private set; }
        public ErrorMessageBase? Error { get; private set; }
        protected ServiceResult()
        {
            Status = ServiceResultStatus.Success;
        }
        protected ServiceResult(ErrorMessageBase erorreMessage)
        {
            Status = (ServiceResultStatus)erorreMessage.Status;
            Error = erorreMessage;
        }

        public static IServiceResult Success()
        {
            return new ServiceResult();
        }

        public static IServiceResult Failure(ErrorMessageBase erorreMessage)
        {
            return new ServiceResult(erorreMessage);
        }
    }
    internal class ServiceResult<TResult> : ServiceResult, IServiceResult<TResult>
    {
        public TResult? Data { get; private set; }
        protected ServiceResult(TResult data)
        {
            Data = data;
        }
        protected ServiceResult(ErrorMessageBase erorreMessage) : base(erorreMessage)
        {
        }

        public static IServiceResult<TResult> Success(TResult data)
        {
            return new ServiceResult<TResult>(data);
        }
        public static new IServiceResult<TResult> Failure(ErrorMessageBase erorreMessage)
        {
            return new ServiceResult<TResult>(erorreMessage);
        }
    }
}
