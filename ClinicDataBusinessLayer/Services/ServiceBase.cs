using AutoMapper;
using Azure;
using ClinicDataAccessLayer.Entities.Contracts;
using ClinicDataBusinessLayer.DTOs.Contracts;
using ClinicDataBusinessLayer.Extensions;
using ClinicDataBusinessLayer.Services.ServiceResults;
using ClinicDataBusinessLayer.Services.ServiceResults.Factories;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;
using Microsoft.Extensions.Logging;
using System;

namespace ClinicDataBusinessLayer.Services
{
    public partial class ServiceBase
    {
        private readonly ServiceResultHandlerFactory _serviceResultHandlerFactory;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        protected ServiceBase(ServiceResultHandlerFactory serviceResultHandlerFactory, IMapper mapper, ILogger logger)
        {
            _serviceResultHandlerFactory = serviceResultHandlerFactory;
            _mapper = mapper;
            _logger = logger;
        }
        private async Task<IServiceResult> ExecuteOperationAsync(        
            Delegate operation,
            string operationName = "",
            params ServiceResultHandler[] args)
        {
            try
            {
                var result = operation.DynamicInvoke(args);

                if (result is Task<IServiceResult> asyncResult)
                {
                    return await asyncResult;
                }
                else if (result is IServiceResult syncResult)
                {
                    return syncResult;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception ex)
            {
                var serverErrorMessage = new ServerErrorMessage(ex, "an error has occurred", "an unexpected error has occurred.");
                _logger.LogServerError(serverErrorMessage, operationName);               
                return ServiceResult.Failure(serverErrorMessage);
            }
        }
        private async Task<IServiceResult<TResult>> ExecuteOperationAsync<TResult>(        
            Delegate operation,
            string operationName = "",
            params ServiceResultHandler[] args)
        {
            try
            {
                var result = operation.DynamicInvoke(args);

                if (result is Task<IServiceResult<TResult>> asyncResult)
                {
                    return await asyncResult;
                }
                else if (result is IServiceResult<TResult> syncResult)
                {
                    return syncResult;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception ex)
            {
                var serverErrorMessage = new ServerErrorMessage(ex, "an error has occurred", "an unexpected error has occurred.");
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
        protected async Task<IServiceResult> ExecuteOperationAsync<TServiceMessages>(
            Func<ServiceResultHandler<TServiceMessages>, Task<IServiceResult>> operation,
            string operationName = "")
              where TServiceMessages : class, IServiceMessages
        {
            var serviceBaseResult = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages>();

            return await ExecuteOperationAsync(operation, operationName, serviceBaseResult);
        }

        protected async Task<IServiceResult> ExecuteOperationAsync<TServiceMessages1, TServiceMessages2>(
            Func<ServiceResultHandler<TServiceMessages1>, ServiceResultHandler<TServiceMessages2>, Task<IServiceResult>> operation,
            string operationName = "")
              where TServiceMessages1 : class, IServiceMessages
              where TServiceMessages2 : class, IServiceMessages
        {
            var serviceBaseResult1 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages1>();
            var serviceBaseResult2 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages2>();

            return await ExecuteOperationAsync(operation, operationName, serviceBaseResult1, serviceBaseResult2);
        }

        protected async Task<IServiceResult<TResult>> ExecuteOperationAsync<TResult, TServiceMessages>(
            Func<ServiceResultHandler<TServiceMessages>, Task<IServiceResult<TResult>>> operation,
            string operationName = "")
             where TResult : class
             where TServiceMessages : class, IServiceMessages
        {
            var serviceBaseResult = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages>();

            return await ExecuteOperationAsync<TResult>(operation, operationName, serviceBaseResult);
        }
        protected async Task<IServiceResult<TResult>> ExecuteOperationAsync<TResult, TServiceMessages1, TServiceMessages2>(
            Func<ServiceResultHandler<TServiceMessages1>, ServiceResultHandler<TServiceMessages2>, Task<IServiceResult<TResult>>> operation,
            string operationName = "")
              where TServiceMessages1 : class, IServiceMessages
              where TServiceMessages2 : class, IServiceMessages
        {
            var serviceBaseResult1 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages1>();
            var serviceBaseResult2 = _serviceResultHandlerFactory.CreateServiceResultHandler<TServiceMessages2>();

            return await ExecuteOperationAsync<TResult>(operation, operationName, serviceBaseResult1, serviceBaseResult2);
        }
        protected bool AreDtoToEntryPathsCompatible<TEntry, TRequestDto, TDtoResult>()
            where TEntry : IEntry
            where TRequestDto : IRequestDto
            where TDtoResult : IDto
        {
            var dtoToEntryPaths = PropertyPathResolver
                                    .ExtractDtoToEntryPropertyPaths(_mapper.ConfigurationProvider, typeof(TRequestDto), typeof(TEntry));
            var entryToDtoPaths = PropertyPathResolver
                                    .ExtractEntryToDtoPropertyPaths(_mapper.ConfigurationProvider, typeof(TEntry), typeof(TDtoResult));

            return entryToDtoPaths.All(dtoToEntryPaths.Contains);
        }

    }
}
