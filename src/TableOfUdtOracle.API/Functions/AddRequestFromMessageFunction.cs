using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using TableOfUdtOracle.ApplicationCore.Dtos.Requests;
using TableOfUdtOracle.ApplicationCore.Services.Interfaces;

namespace TableOfUdtOracle.Functions.API.Functions
{
    public class AddRequestFromMessageFunction
    {
        private readonly ILogger<AddRequestFromMessageFunction> _logger;
        private readonly ITableOfUdtOracleServices _services;

        public AddRequestFromMessageFunction(ILogger<AddRequestFromMessageFunction> logger, ITableOfUdtOracleServices services)
        {
            _logger = logger;
            _services = services;
        }

        [Function(nameof(AddRequestFromMessageFunction))]
        public async Task Run(
            [ServiceBusTrigger("mytopic", "mysubscription", Connection = "")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var requestFromMessage = JsonConvert.DeserializeObject<TableOfUdtOracleRequest>(Encoding.UTF8.GetString(message.Body));
            
            _logger.LogInformation($"Deserialization of message to request instance completed,  {nameof(requestFromMessage)}");

            var result = await _services.AddDemandeTableOfUdtOracleAsync(requestFromMessage);
            
            if (result is null || !result.IsSuccess) 
            { 
                _logger.LogError($"An error occured: {result.Message}"); 
                throw new InvalidOperationException($"An error occured: {result.Message}"); 
            }
            
            _logger.LogInformation(result.Message);
            
             // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
