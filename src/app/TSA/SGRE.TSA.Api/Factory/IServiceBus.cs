using Azure.Messaging.ServiceBus;

namespace SGRE.TSA.Api.Factory
{
    public interface IServiceBus
    {
        void SetServiceBusClient();
        ServiceBusSender SetServiceBusSender();
    }
}
