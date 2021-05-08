using Microsoft.Extensions.Logging;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class WtgCatalogueExternalService : IWtgCatalogueExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<WtgCatalogueExternalService> logger;
        public WtgCatalogueExternalService(IHttpClientFactory httpClientFactory, ILogger<WtgCatalogueExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<ExternalServiceResponse<IEnumerable<WtgCatalogue>>> GetWtgCatalogueAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/WtgCatalogue/?$expand=towertypes($expand=noisemodes,towerSections),applicationmodes,nacelledistances");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<WtgCatalogue>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<WtgCatalogue>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<WtgCatalogue>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<WtgCatalogue>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<WtgCatalogue>> GetWtgCatalogueAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/WtgCatalogue/{id}?$expand=applicationModes");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<List<WtgCatalogue>>(content, options);

                    return new ExternalServiceResponse<WtgCatalogue>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result[0]
                    };
                }

                return new ExternalServiceResponse<WtgCatalogue>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<WtgCatalogue>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<WtgThreshold>> GetWtgThresholdAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/WtgThreshold");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<List<WtgThreshold>>(content, options);

                    return new ExternalServiceResponse<WtgThreshold>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result[0]
                    };
                }

                return new ExternalServiceResponse<WtgThreshold>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<WtgThreshold>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

    }
}
