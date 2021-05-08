using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Factory
{
    public interface IServiceBusFactory
    {
        public IServiceBus GetServiceBus(string SBType);
    }
}
