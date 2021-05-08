using Microsoft.Extensions.Logging;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class CountryExternalService : ICountryExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CountryExternalService> logger;
        public CountryExternalService(IHttpClientFactory httpClientFactory, ILogger<CountryExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<ExternalServiceResponse<IEnumerable<Country>>> GetCountryAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");
                
                var response = await client.GetAsync($"api/Country/?$expand=region");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Country>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Country>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Country>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Country>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
        public async Task<ExternalServiceResponse<IEnumerable<Country>>> GetCountryAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");
                var response = await client.GetAsync($"api/Country/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Country>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Country>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Country>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Country>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Country>>> GetCountryByRegionAsync(int regionId)
        {
            try
            {

                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Country?$filter=regionId eq {regionId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Country>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Country>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Country>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Country>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

    }
}
