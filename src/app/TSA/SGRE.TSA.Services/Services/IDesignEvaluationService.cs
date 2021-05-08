using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IDesignEvaluationService
    {
        Task<(bool IsSuccess, IEnumerable<RequestDesignEvaluation> requestDesignResult)> GetRequestDesignAsync(int sstId);
        Task<(bool IsSuccess, IEnumerable<Summary> DESumResults)> GetDESummaryIdAsync(int id);
        Task<(bool IsSuccess, IEnumerable<SstDesignEvaluation> DesignEvaluation)> GetDesignEvaluationByIdAsync(int id);
        Task<(bool IsSuccess, dynamic DesignEvaluationResults)> PutDesignEvaluationAsync(SstDesignEvaluation sstDesignEvaluation);
        Task<(bool IsSuccess, dynamic DesignEvaluationResults)> PatchDesignEvaluationAsync(SstDesignEvaluation sstDesignEvaluation);
        Task<(bool IsSuccess, IEnumerable<SegmentDimensionSummary> segmentDimensionSummaryResult)> GetSegmentDimensionSummaryAsync(int sstId);
        Task<(bool IsSuccess, IEnumerable<SegmentDimensionTable> segmentDimensionTableResult)> GetSegmentDimensionTableAsync(string SstUuid);
        Task<RequestDesignEvaluation> updateMessageData(RequestDesignEvaluation dataToSend, IProjectConstraintsService projectConstraintService, IGMBService gMBService, IOpportunityService opportunityService);


    }
}
