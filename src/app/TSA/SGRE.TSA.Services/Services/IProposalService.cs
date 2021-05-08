using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IProposalService
    {
        Task<(bool IsSuccess, IEnumerable<Proposal> ProposalResults)> GetProposalByOpportunityAsync(int opportunityId);
        Task<(bool IsSuccess, IEnumerable<Proposal> ProposalResults)> GetProposalByIdAsync(int id);
        Task<(bool IsSuccess, IEnumerable<Proposal> ProposalResults)> GetProposalByOpportunityProposalIdAsync(int opportunityId, int proposalId);
        Task<(bool IsSuccess, dynamic proposalResults)> PutProposalAsync(Proposal proposal);
        Task<(bool IsSuccess, dynamic proposalResults)> PatchProposalAsync(int patchId, Proposal proposal);
    }
}
