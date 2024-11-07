using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ClinicDataBusinessLayer.Services.ServiceResults.Factories;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts;


namespace ClinicDataBusinessLayer.Extensions
{
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
                .AddClasses(classes => classes.AssignableTo<ITransientMessage>())
                .AsSelf()
                .WithTransientLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<IScopedMessage>())
                .AsSelf()
                .WithScopedLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<ISingletonMessage>())
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
}
