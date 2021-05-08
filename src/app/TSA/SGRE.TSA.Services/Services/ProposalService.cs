using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SGRE.TSA.Services.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IProposalExternalService proposalExternalService;

        public ProposalService(IProposalExternalService proposalExternalService)
        {
            this.proposalExternalService = proposalExternalService;
        }

        public async Task<(bool IsSuccess, IEnumerable<Proposal> ProposalResults)> GetProposalByOpportunityAsync(int opportunityId)
        {
            var proposalResult = await proposalExternalService.GetProposalByOpportunityAsync(opportunityId);
            if (proposalResult.IsSuccess)
            {
                return (true, proposalResult.ResponseData);
            }
            return (false, null);

        }
        public async Task<(bool IsSuccess, IEnumerable<Proposal> ProposalResults)> GetProposalByIdAsync(int id)
        {
            var proposalResult = await proposalExternalService.GetProposalByIdAsync(id);
            if (proposalResult.IsSuccess)
            {
                return (true, proposalResult.ResponseData);
            }
            return (false, null);

        }

        public async Task<(bool IsSuccess, IEnumerable<Proposal> ProposalResults)> GetProposalByOpportunityProposalIdAsync(int opportunityId, int proposalId)
        {
            var proposalResult = await proposalExternalService.GetProposalByOpportunityProposalIdAsync(opportunityId, proposalId);
            if (proposalResult.IsSuccess)
            {
                return (true, proposalResult.ResponseData);
            }
            return (false, null);
        }


        public async Task<(bool IsSuccess, dynamic proposalResults)> PatchProposalAsync(int patchId, Proposal proposal)
        {
            var proposalData = await proposalExternalService.GetProposalByIdAsync(patchId);

            if (proposalData.IsSuccess && proposalData.ResponseData.FirstOrDefault().CertificationId != proposal.CertificationId)
            {
                await proposalExternalService.UpdateCertficationCost(patchId, proposal.CertificationId);
            }

            var proposalResult = await proposalExternalService.PatchProposalAsync(patchId, proposal);
            if (proposalResult.IsSuccess)
            {
                var result = new
                {
                    project = proposalResult.ResponseData
                };
                return (true, result);
            }

            return (false, proposalResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, dynamic proposalResults)> PutProposalAsync(Proposal proposal)
        {
            var proposalResult = await proposalExternalService.PutProposalAsync(proposal);
            if (proposalResult.IsSuccess)
            {
                var result = new
                {
                    project = proposalResult.ResponseData
                };
                return (true, result);
            }

            return (false, proposalResult.ErrorMessage);
        }
    }
}
