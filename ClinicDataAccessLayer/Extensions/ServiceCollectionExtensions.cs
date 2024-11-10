namespace ClinicDataAccessLayer.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDtaAccessLayerServices(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<AppDbContext>(option =>
        {
            option.UseSqlServer(connectionString)
                .AddInterceptors(new SoftDeleteInterceptor());
        });

        return services;
    }
}