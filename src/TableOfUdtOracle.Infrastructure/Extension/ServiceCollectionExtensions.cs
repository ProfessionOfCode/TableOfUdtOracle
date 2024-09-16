using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TableOfUdtOracle.Infrastructure.Interface;

namespace TableOfUdtOracle.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString, bool useInMemory=false)
        {
            services.AddDbContext<TableOfUdtOracleDbContext>(options =>
            {
                if (useInMemory)
                {
                    options.UseInMemoryDatabase("InMemeoryDbForTesting").
                    ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                }
                else
                {
                    options.UseOracle(connectionString,
                        builder => builder.MigrationsAssembly(typeof(TableOfUdtOracleDbContext).Assembly.FullName)
                        .UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19));
                }
            });

            //services.AddHealthChecks().();
            services.AddScoped<ITableOfUdtOracleDbContext>(provider =>            
                provider.GetService<TableOfUdtOracleDbContext>()!
            );
            return services;
        }
    }
}
