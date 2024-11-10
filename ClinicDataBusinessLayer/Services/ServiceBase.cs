namespace ClinicDataBusinessLayer.Services;

public class ServiceBase
{
    private readonly ServiceResultHandlerFactory _serviceResultHandlerFactory;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ServerErrorMessages _serverErrorMessages;

    protected ServiceBase(ServiceResultHandlerFactory serviceResultHandlerFactory,
        IMapper mapper,
        ILogger logger,
        ServerErrorMessages serverErrorMessages)
    {
        _serviceResultHandlerFactory = serviceResultHandlerFactory;
        _mapper = mapper;
        _logger = logger;
        _serverErrorMessages = serverErrorMessages;
    }
    private async Task<IServiceResult> ExecuteOperationAsync(        
        Delegate operation,
        string operationName = "",
        params object?[]? args)
    {
        try
        {
            return operation.DynamicInvoke(args) switch
            {
                Task<IServiceResult> asyncResult => await asyncResult,
                IServiceResult result => result,
                _ => throw new InvalidOperationException()
            };
        }
        catch (Exception ex)
        {
            
            var serverErrorMessage = _serverErrorMessages.ServerError(ex);
            _logger.LogServerError(serverErrorMessage, operationName);               
            return ServiceResult.Failure(serverErrorMessage);
        }
    }
    private async Task<IServiceResult<TResult>> ExecuteOperationAsync<TResult>(        
        Delegate operation,
        string operationName = "",
        params object?[]? args)
    {
        try
        {
            return operation.DynamicInvoke(args) switch
            {
                Task<IServiceResult<TResult>> asyncResult => await asyncResult,
                IServiceResult<TResult> result => result,
                _ => throw new InvalidOperationException()
            };
        }
        catch (Exception ex)
        {
            var serverErrorMessage = _serverErrorMessages.ServerError(ex);
            _logger.LogServerError(serverErrorMessage, operationName);               
            return ServiceResult<TResult>.Failure(serverErrorMessage);
        }
    }
    
    protected async Task<IServiceResult> ExecuteOperationAsync(
        Func<ServiceResultHandler, Task<IServiceResult>> operation,
        string operationName = "")
    {
        var serviceBaseResult = _serviceResultHandlerFactory.CreateServiceResultHandler();

        return await ExecuteOperationAsync(operation, operationName, serviceBaseResult);
    }

    protected async Task<IServiceResult<TResult>> ExecuteOperationAsync<TResult>(
        Func<ServiceResultHandler, Task<IServiceResult<TResult>>> operation,
        string operationName = "")
        where TResult : class
    {
        var serviceBaseResult = _serviceResultHandlerFactory.CreateServiceResultHandler();

        return await ExecuteOperationAsync<TResult>(operation, operationName, serviceBaseResult);
    }
    protected async Task<IServiceResult> ExecuteOperationAsync<TServiceErrorMessages>(
        Func<ServiceResultHandler<TServiceErrorMessages>, Task<IServiceResult>> operation,
        string operationName = "")
        where TServiceErrorMessages : class, IServiceErrorMessages
    {
        var serviceBaseResult = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceErrorMessages>();

        return await ExecuteOperationAsync(operation, operationName, serviceBaseResult);
    }

    protected async Task<IServiceResult> ExecuteOperationAsync<TServiceMessages1, TServiceMessages2>(
        Func<ServiceResultHandler<TServiceMessages1>, ServiceResultHandler<TServiceMessages2>, Task<IServiceResult>> operation,
        string operationName = "")
        where TServiceMessages1 : class, IServiceErrorMessages
        where TServiceMessages2 : class, IServiceErrorMessages
    {
        var serviceBaseResult1 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages1>();
        var serviceBaseResult2 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages2>();

        return await ExecuteOperationAsync(operation, operationName, serviceBaseResult1, serviceBaseResult2);
    }

    protected async Task<IServiceResult<TResult>> ExecuteOperationAsync<TResult, TServiceErrorMessages>(
        Func<ServiceResultHandler<TServiceErrorMessages>, Task<IServiceResult<TResult>>> operation,
        string operationName = "")
        where TResult : class
        where TServiceErrorMessages : class, IServiceErrorMessages
    {
        var serviceBaseResult = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceErrorMessages>();

        return await ExecuteOperationAsync<TResult>(operation, operationName, serviceBaseResult);
    }
    protected async Task<IServiceResult<TResult>> ExecuteOperationAsync<TResult, TServiceErrorMessages1, TServiceErrorMessages2>(
        Func<ServiceResultHandler<TServiceErrorMessages1>, ServiceResultHandler<TServiceErrorMessages2>, Task<IServiceResult<TResult>>> operation,
        string operationName = "")
        where TServiceErrorMessages1 : class, IServiceErrorMessages
        where TServiceErrorMessages2 : class, IServiceErrorMessages
    {
        var serviceBaseResult1 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceErrorMessages1>();
        var serviceBaseResult2 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceErrorMessages2>();

        return await ExecuteOperationAsync<TResult>(operation, operationName, serviceBaseResult1, serviceBaseResult2);
    }
    protected bool AreDtoToEntryPathsCompatible<TEntry, TRequestDto, TDtoResult>()
        where TEntry : IEntry
        where TRequestDto : IRequestDto
        where TDtoResult : IDto
    {
        var dtoToEntryPaths = EntityDtoPathExtractor
            .ExtractDtoToEntryPropertyPaths(_mapper.ConfigurationProvider, typeof(TRequestDto), typeof(TEntry));
        var entryToDtoPaths = EntityDtoPathExtractor
            .ExtractEntryToDtoPropertyPaths(_mapper.ConfigurationProvider, typeof(TEntry), typeof(TDtoResult));

        return entryToDtoPaths.All(dtoToEntryPaths.Contains);
    }

}