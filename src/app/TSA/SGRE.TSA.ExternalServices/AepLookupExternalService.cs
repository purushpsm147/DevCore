using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class AepLookupExternalService : IAepLookupExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<AepLookupExternalService> logger;

        public AepLookupExternalService(IHttpClientFactory httpClientFactory, ILogger<AepLookupExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<ExternalServiceResponse<SSTAepLookupGross>> PutAepAsync(SSTAepLookupGross aepLookupGross)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(aepLookupGross, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/AEPLookup", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<SSTAepLookupGross>(content, options);

                    return new ExternalServiceResponse<SSTAepLookupGross>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<SSTAepLookupGross>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<SSTAepLookupGross>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }
    }
}
