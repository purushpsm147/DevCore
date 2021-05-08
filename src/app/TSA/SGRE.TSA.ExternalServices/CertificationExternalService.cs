using Microsoft.Extensions.Logging;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class CertificationExternalService : ICertificationExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CertificationExternalService> logger;

        public CertificationExternalService(IHttpClientFactory httpClientFactory, ILogger<CertificationExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<ExternalServiceResponse<IEnumerable<Certification>>> GetCertificationAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Certification?$expand=CertificationCost");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Certification>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Certification>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Certification>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Certification>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
        public async Task<ExternalServiceResponse<IEnumerable<Certification>>> GetCertificationAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Certification/{id}?$expand=CertificationCost");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Certification>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Certification>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Certification>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Certification>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
    }
}
