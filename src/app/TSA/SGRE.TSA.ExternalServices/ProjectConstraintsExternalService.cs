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
    public class ProjectConstraintsExternalService : IProjectConstraintsExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProjectConstraintsExternalService> logger;
        public ProjectConstraintsExternalService(IHttpClientFactory httpClientFactory, ILogger<ProjectConstraintsExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<ExternalServiceResponse<IEnumerable<ProjectConstraint>>> GetProjectConstraintsAsync(int projectId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/ProjectConstraint/?$filter=projectId eq {projectId} &&$expand=PermitsSalesConstraint,LogisticConstraint($expand=logisticProjectBoundaries),ConstructionConstraint,SpecialRequirementsSalesConstraint");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ProjectConstraint>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<ProjectConstraint>>> GetProjectConstraintsByIdAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/ProjectConstraint/?$filter=Id eq {id} &&$expand=PermitsSalesConstraint,LogisticConstraint($expand=logisticProjectBoundaries),ConstructionConstraint,SpecialRequirementsSalesConstraint");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ProjectConstraint>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<ProjectConstraint>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<ProjectConstraint>> PatchProjectConstraintsAsync(int id, ProjectConstraint projectConstraint)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(projectConstraint, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PatchAsync($"api/ProjectConstraint/{id}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<ProjectConstraint>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = projectConstraint
                    };
                }

                return new ExternalServiceResponse<ProjectConstraint>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<ProjectConstraint>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<ProjectConstraint>> PutProjectConstraintsAsync(ProjectConstraint projectConstraint)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(projectConstraint, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/ProjectConstraint", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<ProjectConstraint>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = projectConstraint
                    };
                }

                return new ExternalServiceResponse<ProjectConstraint>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<ProjectConstraint>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }


    }
}
