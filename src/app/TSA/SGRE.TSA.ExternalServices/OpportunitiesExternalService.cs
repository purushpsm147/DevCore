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
    public class OpportunitiesExternalService : IOpportunitiesExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OpportunitiesExternalService> logger;
        private readonly ISupportExternalService _supportExternalService;
        private readonly IRoleExternalService _roleExternalService;
        private readonly IConfiguration _configuration;

        //create ITSA Data Servicem, Search for ODataClient use it,
        public OpportunitiesExternalService(IHttpClientFactory httpClientFactory, ILogger<OpportunitiesExternalService> logger, ISupportExternalService supportExternalService, IRoleExternalService roleExternalService, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            _supportExternalService = supportExternalService;
            _roleExternalService = roleExternalService;
            _configuration = configuration;
        }

        #region Get Opportunity

        public async Task<ExternalServiceResponse<IEnumerable<Project>>> GetProjectsAsync(string queryParam = "")
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                //TODO: Take care of magic strings
                var response = await client.GetAsync($"api/Opportunities/?{queryParam}&&$expand=country($expand=region),proposals,ProjectMileStones");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Project>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Project>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Project>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Project>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Project>>> GetProjectsByUserAsync(string user)
        {
            try
            {
                var projectResponse = await GetProjectsAsync($"$expand=ProjectRoles");

                if (projectResponse.IsSuccess)
                {
                    return new ExternalServiceResponse<IEnumerable<Project>>()
                    {
                        IsSuccess = true,
                        ResponseData = projectResponse.ResponseData.Where(k => k.ProjectRoles.Any(pr => pr.userId.Equals(user, StringComparison.OrdinalIgnoreCase))),
                        ErrorMessage = null
                    };
                }

                return projectResponse;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Project>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Project>>> GetProjectsAsync(int? id)
        {
            try
            {
                var projectResponse = await GetProjectsAsync($"$filter=id eq {id}&$expand=ProjectRoles,ProjectMileStones,proposals($expand=Certification($expand=CertificationCost))");

                if (projectResponse.IsSuccess)
                {
                    return new ExternalServiceResponse<IEnumerable<Project>>()
                    {
                        IsSuccess = true,
                        ResponseData = projectResponse.ResponseData,
                        ErrorMessage = null
                    };
                }

                return projectResponse;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Project>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        #endregion

        public async Task<ExternalServiceResponse<Project>> PatchProjectsAsync(int id, Project project)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(project, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var initialProjectResponse = await GetProjectsAsync(id);

                Project initialProject = initialProjectResponse.ResponseData.FirstOrDefault();


                var existingRolesInDB = initialProject.ProjectRoles;

                List<ProjectRoles> toBeDeletedRoles = existingRolesInDB.Where(c => !project.ProjectRoles.Any(d => c.Id == d.Id)).ToList();

                var toBeAddedRoles = project.ProjectRoles.Where(c => c.Id == 0);

                var toBeUpdatedRoles = from erl in existingRolesInDB
                                       join rl in project.ProjectRoles
                                         on erl.Id equals rl.Id
                                       where !erl.Equals(rl)
                                       select rl;


                if (toBeDeletedRoles.Count > 0)
                {
                    var jsonRoles = JsonConvert.SerializeObject(toBeDeletedRoles, Formatting.Indented);

                    var httpContentDelete = new StringContent(jsonRoles, Encoding.UTF8, "application/json");

                    var deleteResponse = await client.PostAsync($"api/AssignRoles/DeleteRoles", httpContentDelete);

                }

                var response = await client.PatchAsync($"api/Opportunities/{id}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (toBeUpdatedRoles.Any() || toBeAddedRoles.Any())
                        await SendMail(project, "Updated opportunity", toBeAddedRoles.Concat(toBeUpdatedRoles));

                    return new ExternalServiceResponse<Project>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = project
                    };
                }

                return new ExternalServiceResponse<Project>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Project>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<Project>> PutProjectsAsync(Project project)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(project, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/Opportunities", httpContent);



                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<Project>(content, options);
                    if (project.ProjectRoles.Any())
                    {
                        project.Id = result.Id;
                        await SendMail(project, "Added new opportunity", project.ProjectRoles);
                    }

                    return new ExternalServiceResponse<Project>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = project
                    };
                }

                return new ExternalServiceResponse<Project>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Project>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        private async System.Threading.Tasks.Task SendMail(Project project, string mailSubject, IEnumerable<ProjectRoles> projectRoles)
        {
            if (projectRoles.Any())
            {
                //Get all roles
                var results = await _roleExternalService.GetRoleAsync();
                IEnumerable<Role> allRoles = results.ResponseData;
                string UiBaseUrl = _configuration.GetSection("UI").Value;

                //need to send mail
                MailBody mailBody;

                Parallel.ForEach(projectRoles, async (item) =>
                {
                    if (!string.IsNullOrWhiteSpace(item.userId))
                    {
                        mailBody = new MailBody
                        {
                            CustomerProject = project.ProjectName,
                            Requestedby = project.CreatedBy,
                            Startdate = DateTime.UtcNow.ToString(),
                            To = item.userId,
                            ToRole = allRoles.FirstOrDefault(r => r.Id == item.RoleId).RoleName,
                            Subject = mailSubject,
                            TowerScenario = "NA",
                            QuoteId = "NA",
                            Workflow = "Role assigned",
                            Responsible = item.userId,
                            ReassignmentLink = $"{UiBaseUrl}opportunity/reassign/{project.Id}/generalassignment",
                            UILInk=UiBaseUrl
                        };
                        await _supportExternalService.SendMail(mailBody);
                    }
                });
            }
        }

        public async Task<bool> GetCurrencyLockedDetails(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Opportunities/IsCostFeedbackAvailable/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)                  
                    return true;
                
                return false;

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return false;

            }
        }
    }
}

