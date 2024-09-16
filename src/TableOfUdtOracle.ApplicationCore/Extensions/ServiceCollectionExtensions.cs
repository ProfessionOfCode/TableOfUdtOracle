using Microsoft.Extensions.DependencyInjection;
using TableOfUdtOracle.ApplicationCore.Services;
using TableOfUdtOracle.ApplicationCore.Services.Interfaces;

namespace TableOfUdtOracle.ApplicationCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // add application services like AUTOMAPPER, FluentValidation or add all validators through assembly            
            services.AddScoped<ITableOfUdtOracleServices>(provider =>
            {
                return provider.GetService<TableOfUdtOracleServices>();
            });
            return services;
        }
    }
}
