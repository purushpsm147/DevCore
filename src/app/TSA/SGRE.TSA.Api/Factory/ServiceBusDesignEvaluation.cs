using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;

namespace SGRE.TSA.Api.Factory
{
    public class ServiceBusDesignEvaluation : IServiceBus
    {
        public ServiceBusClient serviceBusClient;
        public ServiceBusSender serviceBusSender;
        private readonly IConfiguration _configuration;
        public ServiceBusDesignEvaluation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SetServiceBusClient()
        {
            var options = new ServiceBusClientOptions()
            {
                //verify values.
                RetryOptions = new ServiceBusRetryOptions
                {
                    Mode = ServiceBusRetryMode.Exponential,
                    MaxRetries = 3,
                    Delay = TimeSpan.FromMilliseconds(300),
                    MaxDelay = TimeSpan.FromSeconds(5),
                    TryTimeout = TimeSpan.FromSeconds(30)

                }
            };
            serviceBusClient = new ServiceBusClient(_configuration.GetSection("ServiceBus:TowersiteInterfaceServiceBusConnectionString").Value, options);
        }
        public ServiceBusSender SetServiceBusSender()
        {
            serviceBusSender = serviceBusClient.CreateSender(_configuration.GetSection("ServiceBus:TowersiteInterfaceQueueName").Value);
            return serviceBusSender;
        }
    }
}

