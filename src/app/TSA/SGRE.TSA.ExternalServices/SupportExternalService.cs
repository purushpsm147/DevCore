using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace SGRE.TSA.ExternalServices
{
    public class SupportExternalService : ISupportExternalService
    {
        private readonly ILogger<SupportExternalService> _logger;
        private readonly ServiceBusClient serviceBusClient;
        private readonly IConfiguration _configuration;

        public SupportExternalService(IConfiguration configuration, ILogger<SupportExternalService> logger, ServiceBusClient serviceBusClient)
        {
            _configuration = configuration;
            _logger = logger;
            this.serviceBusClient = serviceBusClient;
        }

        public async Task<ExternalServiceResponse<string>> SendMail(MailBody mailBody)
        {
            try
            {
                ServiceBusSender sender = serviceBusClient.CreateSender(_configuration.GetSection("ServiceBus:QueueName").Value);

                ServiceBusMessage message = new ServiceBusMessage(JsonConvert.SerializeObject(mailBody));

                await sender.SendMessageAsync(message);

                return new ExternalServiceResponse<string>()
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    ResponseData = "Message Sent Successfully"
                };

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }
    }
}
