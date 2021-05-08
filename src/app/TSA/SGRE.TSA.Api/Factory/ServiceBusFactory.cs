using Microsoft.Extensions.Configuration;

namespace SGRE.TSA.Api.Factory
{
    public class ServiceBusFactory : IServiceBusFactory
    {
        private IServiceBus serviceBus;
        private readonly IConfiguration _configuration;
        public ServiceBusFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceBus GetServiceBus(string SBType)
        {
            if (SBType.Equals("DesignEvaluation"))
            {
                serviceBus = new ServiceBusDesignEvaluation(_configuration);
            }
            else if (SBType.Equals(" Some othere Service bus here"))
            {
                //serviceBus = new SomeOtherServiceBus();
            }
            return serviceBus;
        }

        //public IServiceBus GetServiceBus(string SBType)
        //{
        //    if (SBType.Equals("DesignEvaluation"))
        //    {
        //        serviceBus = new ServiceBusDesignEvaluation();
        //    }
        //    else if (SBType.Equals(" Some othere Service bus here"))
        //    {
        //        //serviceBus = new SomeOtherServiceBus();
        //    }
        //    return serviceBus;
        //}
    }
}