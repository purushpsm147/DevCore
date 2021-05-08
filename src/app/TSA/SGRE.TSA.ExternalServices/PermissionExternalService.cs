using Microsoft.Extensions.Logging;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class PermissionExternalService : IPermissionExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<PermissionExternalService> logger;


        public PermissionExternalService(IHttpClientFactory httpClientFactory, ILogger<PermissionExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<ExternalServiceResponse<IEnumerable<Permission>>> GetPermissionAsync(int roleId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Permission/?$filter=roleId eq {roleId}&$expand=ProjectModule");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Permission>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Permission>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Permission>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Permission>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
    }
}
