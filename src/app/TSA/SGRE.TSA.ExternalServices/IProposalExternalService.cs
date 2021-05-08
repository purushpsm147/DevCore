using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IProposalExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Proposal>>> GetProposalByOpportunityAsync(int opportunityId);
        Task<ExternalServiceResponse<Proposal>> PutProposalAsync(Proposal proposal);
        Task<ExternalServiceResponse<IEnumerable<Proposal>>> GetProposalByOpportunityProposalIdAsync(int opportunityId, int proposalId);
        Task<ExternalServiceResponse<Proposal>> PatchProposalAsync(int patchId,Proposal proposal);
        Task<ExternalServiceResponse<IEnumerable<Proposal>>> GetProposalByIdAsync(int id);
        Task<bool> UpdateCertficationCost(int projectId, int certificationId);
    }
}
