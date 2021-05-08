using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace SGRE.TSA.ExternalServices
{
    public class ProposalExternalService : IProposalExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProposalExternalService> logger;

        public ProposalExternalService(IHttpClientFactory httpClientFactory, ILogger<ProposalExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<ExternalServiceResponse<IEnumerable<Proposal>>> GetProposalByIdAsync(int id)
        {
            var client = httpClientFactory.CreateClient("ToSAService");

            //TODO: Take care of magic strings
            var response = await client.GetAsync($"api/Proposals/?$filter=id eq {id}&$expand=proposalTasks($expand=task)");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Proposal>>(content, options);

                return new ExternalServiceResponse<IEnumerable<Proposal>>()
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    ResponseData = result
                };
            }

            return new ExternalServiceResponse<IEnumerable<Proposal>>()
            {
                IsSuccess = false,
                ErrorMessage = response.ReasonPhrase,
                ResponseData = null
            };
        }

        public async Task<ExternalServiceResponse<IEnumerable<Proposal>>> GetProposalByOpportunityAsync(int opportunityId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                //TODO: Take care of magic strings
                var response = await client.GetAsync($"api/Proposals/?$filter=projectId eq {opportunityId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Proposal>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Proposal>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Proposal>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Proposal>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Proposal>>> GetProposalByOpportunityProposalIdAsync(int opportunityId, int proposalId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                //TODO: Take care of magic strings
                var response = await client.GetAsync($"api/Proposals/?$filter=projectId eq {opportunityId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Proposal>>(content, options)?.Where(p => p.Id == proposalId);

                    return new ExternalServiceResponse<IEnumerable<Proposal>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Proposal>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Proposal>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<Proposal>> PatchProposalAsync(int patchId, Proposal proposal)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(proposal, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PatchAsync($"api/Proposals/{patchId}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<Proposal>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = proposal
                    };
                }

                return new ExternalServiceResponse<Proposal>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Proposal>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }

        public async Task<ExternalServiceResponse<Proposal>> PutProposalAsync(Proposal proposal)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(proposal, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/Proposals", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<Proposal>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = proposal
                    };
                }

                return new ExternalServiceResponse<Proposal>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Proposal>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
        public async Task<bool> UpdateCertficationCost(int projectId,int certificationId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var httpContent = new StringContent("", Encoding.UTF8, "application/json");

                var response = await client.PatchAsync($"api/Proposals/UpdateCertficationCost/projectId={projectId}&certificationId={certificationId}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

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
