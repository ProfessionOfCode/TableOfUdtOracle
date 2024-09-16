using Microsoft.ApplicationInsights;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Oracle.ManagedDataAccess.Client;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TableOfUdtOracle.Infrastructure.Extensions;
using TableOfUdtOracle.ApplicationCore.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var isDevelopment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development";

        IdentityModelEventSource.ShowPII = true;

        var config = new ConfigurationBuilder()
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets<Program>()
                    .AddEnvironmentVariables()
                    .Build();

            var host = new HostBuilder()
                        .ConfigureFunctionsWebApplication()
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton<TelemetryClient>();

                            services.AddLocalization();
                            //services.Configure<AzureAdOptions>(options => config.Bind("AzureAd", options));
                            //var dbParams = DbContext.GetDbParams
                            //services.AddSeleniumAuthService(config, dbParams);

                            //application extension services added
                            services.AddApplicationServices();  

                            OracleConfiguration.SqlNetAllowedLogonVersionClient = OracleAllowedLogonVersionClient.Version11;

                            //infrastructure extension services added
                            services.AddInfrastructureServices(config["ORACLE_DB_CONNECTION_STRING"]);

                            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

                            services.AddSwaggerGen();

                            services.AddApplicationInsightsTelemetryWorkerService();
                            services.ConfigureFunctionsApplicationInsights();
                        })
                        .Build();

            host.Run();
    }
}