using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace SGRE.TSA.ExternalServices
{
    public class ExternalServiceFactory : IExternalServiceFactory
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly IConfiguration _configuration;
        public ExternalServiceFactory(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
        }

        public IExternalService<T> CreateExternalService<T>(ILogger logger) where T : class
        {
            return new ExternalService<T>(_httpClientFactory, logger, _configuration);
        }
    }


}
