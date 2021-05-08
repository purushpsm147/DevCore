using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace SGRE.TSA.ExternalServices
{
    public class TestExternalService : ExternalService<TestExternalService>
    {

        public TestExternalService(IHttpClientFactory httpClientFactory, ILogger<TestExternalService> logger, IConfiguration configuration)
            :
            base(httpClientFactory, logger, configuration)
        {

        }




    }
}
