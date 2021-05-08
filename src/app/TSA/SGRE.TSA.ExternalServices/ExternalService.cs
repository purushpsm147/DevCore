using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class ExternalService<T> : IExternalService<T>
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logger;
        private readonly IConfiguration _configuration;
        public ExternalService(IHttpClientFactory httpClientFactory, ILogger logger, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this._configuration = configuration;
        }
        public async Task<ExternalServiceResponse<IEnumerable<T>>> GetAsync(string queryPram = "")
        {
            try
            {
                var _externalServiceUrl = _configuration.GetSection("ExternalService").GetSection(typeof(T).Name).Value;

                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/{_externalServiceUrl}{queryPram}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<T>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<T>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<T>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<T>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async virtual Task<Models.ExternalServiceResponse<IEnumerable<T>>> GetByIdAsync(int id)
        {
            try
            {
                var _externalServiceUrl = _configuration.GetSection("ExternalService").GetSection(typeof(T).Name).Value;

                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/{_externalServiceUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<T>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<T>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<T>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<T>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<ExternalServiceResponse<T>> PatchAsync(int id, T model)
        {
            try
            {
                var _externalServiceUrl = _configuration.GetSection("ExternalService").GetSection(typeof(T).Name).Value;

                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(model, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PatchAsync($"api/{_externalServiceUrl}/{id}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<T>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = model
                    };
                }

                return new ExternalServiceResponse<T>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = model
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<T>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = model
                };
            }
        }

        public async Task<ExternalServiceResponse<T>> PatchAsync(T model)
        {
            try
            {
                var _externalServiceUrl = _configuration.GetSection("ExternalService").GetSection(typeof(T).Name).Value;

                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(model, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PatchAsync($"api/{_externalServiceUrl}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<T>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = model
                    };
                }

                return new ExternalServiceResponse<T>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = model
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<T>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = model
                };
            }
        }

        public async Task<ExternalServiceResponse<T>> PutAsync(T model)
        {
            try
            {
                var _externalServiceUrl = _configuration.GetSection("ExternalService").GetSection(typeof(T).Name).Value;

                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(model, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/{_externalServiceUrl}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<T>(content, options);

                    return new ExternalServiceResponse<T>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<T>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = model
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<T>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = model
                };

            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<T>>> DeleteAsync(int id)
        {
            try
            {
                var _externalServiceUrl = _configuration.GetSection("ExternalService").GetSection(typeof(T).Name).Value;

                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.DeleteAsync($"api/{_externalServiceUrl}/{id}");

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());

                    return new ExternalServiceResponse<IEnumerable<T>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = responseData
                    };
                }

                return new ExternalServiceResponse<IEnumerable<T>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<T>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
    }
}
