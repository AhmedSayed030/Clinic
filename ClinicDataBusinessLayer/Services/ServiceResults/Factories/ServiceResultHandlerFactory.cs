namespace ClinicDataBusinessLayer.Services.ServiceResults.Factories;

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
    public ServiceResultHandler<TServiceErrorMessages> CreateServiceResultHandler<TServiceErrorMessages>()
        where TServiceErrorMessages : class, IServiceErrorMessages
    {
        var serverErrors = _serviceProvider.GetService<TServiceErrorMessages>()!;

        return new ServiceResultHandler<TServiceErrorMessages>(serverErrors);
    }
}