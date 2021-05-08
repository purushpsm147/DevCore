using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class LoadsClusterExternalService : ILoadsClusterExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<LoadsClusterExternalService> logger;

        public LoadsClusterExternalService(IHttpClientFactory httpClientFactory, ILogger<LoadsClusterExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<ExternalServiceResponse<IEnumerable<LoadsCluster>>> GetLoadsClusterAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/LoadCluster");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<LoadsCluster>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<LoadsCluster>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<LoadsCluster>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<LoadsCluster>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
      
    }
}
