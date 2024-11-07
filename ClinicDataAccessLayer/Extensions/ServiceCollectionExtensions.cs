using ClinicDataAccessLayer.Data.Interceptors;
using ClinicDataAccessLayer.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClinicDataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddataAccessLayerServices(this IServiceCollection services, string? connectionString)
        {
            return services.AddDbContext<AppDbContext>(option =>
            {

                option.UseSqlServer(connectionString)
                      .AddInterceptors(new SoftDeleteInterceptor());
            });
        }

    }
}
