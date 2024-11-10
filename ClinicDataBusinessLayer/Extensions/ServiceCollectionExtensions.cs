namespace ClinicDataBusinessLayer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<ServiceResultHandlerFactory>();

        var assembly = Assembly.GetExecutingAssembly();

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo<ITransientErrorMessages>())
            .AsSelf()
            .WithTransientLifetime()
        );

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo<IScopedErrorMessages>())
            .AsSelf()
            .WithScopedLifetime()
        );

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo<ISingletonErrorMessages>())
            .AsSelf()
            .WithSingletonLifetime()
        );

        services.Scan(scan => scan
            .FromAssemblies(assembly) 
            .AddClasses(classes => classes.AssignableTo<ITransientService>()) 
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        services.Scan(scan => scan
            .FromAssemblies(assembly) 
            .AddClasses(classes => classes.AssignableTo<IScopedService>()) 
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        services.Scan(scan => scan
            .FromAssemblies(assembly) 
            .AddClasses(classes => classes.AssignableTo<ISingletonService>()) 
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );

        return services;                          
    }          
}