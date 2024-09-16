using Azure.Core.Amqp;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NSubstitute;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.Text;
using TableOfUdtOracle.ApplicationCore.Dtos.Requests;
using TableOfUdtOracle.ApplicationCore.Services;
using TableOfUdtOracle.ApplicationCore.Services.Interfaces;
using TableOfUdtOracle.Functions.API.Functions;
using TableOfUdtOracle.Infrastructure;
using TableOfUdtOracle.Infrastructure.Interface;

namespace TableOfUDTOracleUnitTests
{
    public class AddRequestFromMessageFunctionUnitTests
    {
        private readonly ServiceCollection _serviceCollection;
        private readonly ServiceBusMessageActions _messageAction;
        private readonly ITableOfUdtOracleServices _services;
        private readonly ILogger<AddRequestFromMessageFunction> _logger;
        private readonly AddRequestFromMessageFunction _sut;
        private ServiceBusReceivedMessage _serviceBusMessageReceived;

        private const string skipIntegrationTests = null;       //TODO comment this line before commiting the code
        //private const string skipIntegrationTests = "This will make the tests skipped";   //TODO Uncomment before commiting the code

        public AddRequestFromMessageFunctionUnitTests()
        {
            _serviceCollection = new ServiceCollection();

            var config = new ConfigurationBuilder()
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets<Program>()
                    .AddEnvironmentVariables()
                    .Build();

            OracleConfiguration.TraceLevel = 7;
            OracleConfiguration.TraceFileLocation = @"C:\Traces";

            _serviceCollection.AddDbContext<TableOfUdtOracleDbContext>(options =>
            {
                var connectionString = config["ORACLE_DB_CONNECTION_STRING"];
                options.UseOracle(connectionString,
                        builder => builder.MigrationsAssembly(typeof(TableOfUdtOracleDbContext).Assembly.FullName)
                        .UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19));                
            });
            
            _serviceCollection.AddScoped<ITableOfUdtOracleDbContext>(provider => provider.GetService<TableOfUdtOracleDbContext>()!);

            _serviceCollection.AddScoped<ITableOfUdtOracleServices, TableOfUdtOracleServices>();
            //_serviceCollection.AddScoped<ITableOfUdtOracleServices>(builder =>
            //    builder.GetService<TableOfUdtOracleServices>()!
            //);
            _messageAction = Substitute.For<ServiceBusMessageActions>();
            
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            
            _services = serviceProvider.GetRequiredService<ITableOfUdtOracleServices>();
            _logger = Substitute.For<ILogger<AddRequestFromMessageFunction>>();

            _sut = new AddRequestFromMessageFunction(_logger, _services);
        }


        [Fact(Skip = skipIntegrationTests)]
        public async Task AddNullElements_WithoutError()
        {
            //ARRANGE
            var fileGamingJpegBytes = await File.ReadAllBytesAsync(@"C:\Users\Gaming\source\repos\TableOfUDTOracle\ressource\Gaming.jpeg");
            var request = new TableOfUdtOracleRequest()
            {
                DemandePermis = new DemandePermis() { REQUETE_ID = "FAKE-REQUETEID-SHDE", NOM = "Tamko", PRENOM = "Stephane" },
                DemandePermisDoc = [new DemandePermisDocuments() { DATA_CONTENT = fileGamingJpegBytes, FICH_NOM = "Gaming.jpeg", MIME_TYPE = "jpeg" }]      
            };

            _serviceBusMessageReceived = ServiceBusReceivedMessage.FromAmqpMessage(
                        new AmqpAnnotatedMessage(new AmqpMessageBody([Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request))])),
                        BinaryData.FromString("Token"));

            //ACT
            await _sut.Run(_serviceBusMessageReceived, _messageAction);

            //ASSERT
            Assert.NotNull(_serviceBusMessageReceived);
            Assert.NotNull(_services);
            Assert.NotNull(_sut);
        }
    }
}