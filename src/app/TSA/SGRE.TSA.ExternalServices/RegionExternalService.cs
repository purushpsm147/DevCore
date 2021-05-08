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
    public class RegionExternalService : IRegionExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<RegionExternalService> logger;

        public RegionExternalService(IHttpClientFactory httpClientFactory, ILogger<RegionExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<ExternalServiceResponse<IEnumerable<Region>>> GetRegionsAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Region");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Region>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Region>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Region>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Region>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Region>>> GetRegionsAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Region/{id}");


                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Region>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Region>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Region>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Region>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

    }
}
