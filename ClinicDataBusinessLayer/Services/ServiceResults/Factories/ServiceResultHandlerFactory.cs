using ClinicDataBusinessLayer.Services.ServiceResults;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicDataBusinessLayer.Services.ServiceResults.Factories
{
    public class ServiceResultHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceResultHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ServiceResultHandler CreateServiceResultHandler()
        {
            return new ServiceResultHandler();
        }
        public ServiceResultHandler<TServeMessages> CreateServiceResultHandler<TServeMessages>()
             where TServeMessages : class, IServiceMessages
        {
            var serverErrors = _serviceProvider.GetService<TServeMessages>()!;

            return new ServiceResultHandler<TServeMessages>(serverErrors);
        }
    }
}
