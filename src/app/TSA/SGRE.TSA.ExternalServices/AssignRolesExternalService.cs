using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class AssignRolesExternalService : IAssignRolesExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<AssignRolesExternalService> logger;
        private readonly ISupportExternalService supportExternalService;
        private readonly IRoleExternalService roleExternalService;
        private readonly IOpportunitiesExternalService opportunities;

        public AssignRolesExternalService(IHttpClientFactory httpClientFactory, ILogger<AssignRolesExternalService> logger, ISupportExternalService supportExternalService, IRoleExternalService roleExternalService, IOpportunitiesExternalService opportunities)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.supportExternalService = supportExternalService;
            this.roleExternalService = roleExternalService;
            this.opportunities = opportunities;
        }
        public async Task<ExternalServiceResponse<IEnumerable<ProjectRoles>>> PostRolesAsync(IEnumerable<ProjectRoles> projectRoles)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");
                
                var jsonString = JsonConvert.SerializeObject(projectRoles, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"api/AssignRoles", httpContent);

                //Add email service after DB verificaion

                if (response.IsSuccessStatusCode)
                {
                    var result = await SendMail(projectRoles);
                    if(!result)
                    {
                        return new ExternalServiceResponse<IEnumerable<ProjectRoles>>()
                        {
                            IsSuccess = false,
                            ErrorMessage = "Server Error, Error in sending mail",
                            ResponseData = projectRoles
                        };
                    }
                    return new ExternalServiceResponse<IEnumerable<ProjectRoles>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = projectRoles
                    };
                }

                return new ExternalServiceResponse<IEnumerable<ProjectRoles>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<ProjectRoles>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }


        private async System.Threading.Tasks.Task<bool> SendMail(IEnumerable<ProjectRoles> projectRoles)
        {
            //Get all roles
            if(projectRoles == null && projectRoles.Any())
            {
                return false;
            }

            var resultRoles = await roleExternalService.GetRoleAsync();

            var projectId = projectRoles.FirstOrDefault()?.ProjectId;
            if (projectId == null) 
            {
                return false;
            }
            var projectdetails = await opportunities.GetProjectsAsync(projectId);

            IEnumerable<Role> allRoles = resultRoles.ResponseData;
            Project project = projectdetails.ResponseData.FirstOrDefault();

            //need to send mail
            MailBody mailBody;

            Parallel.ForEach(projectRoles, async (item) =>
            {
                if (!string.IsNullOrWhiteSpace(item.userId))
                {
                    mailBody = new MailBody
                    {
                        Subject = "Change of Role",
                        Responsible = item.userId,
                        CustomerProject = project.ProjectName,
                        Requestedby = project.CreatedBy,
                        Startdate = DateTime.UtcNow.ToString(),
                        To = item.userId,
                        ToRole = allRoles.FirstOrDefault(r => r.Id == item.RoleId).RoleName,
                        TowerScenario = "NA",
                        QuoteId = "NA",
                        Workflow = "Role assigned"
                    };
                    await supportExternalService.SendMail(mailBody);
                }
            });

            return true;
        }

    }
}
