using Microsoft.Extensions.Logging;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class GMBExternalService : IGMBExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<GMBExternalService> logger;

        public GMBExternalService(IHttpClientFactory httpClientFactory, ILogger<GMBExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<ExternalServiceResponse<IEnumerable<GenericMarketBoundary>>> GetGMBAsync(int CountryId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"/api/GenericMarketBoundary?$filter=CountryId eq {CountryId}&&$expand=transportMode");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<GenericMarketBoundary>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<GenericMarketBoundary>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<GenericMarketBoundary>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<GenericMarketBoundary>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
    }
}
