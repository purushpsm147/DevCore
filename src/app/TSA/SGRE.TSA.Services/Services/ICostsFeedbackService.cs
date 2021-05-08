using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsFeedbackService
    {
        Task<(bool IsSuccess, dynamic costFeebackResults)> PutCostFeebackAsync(CostFeedback costFeeback);
        Task<(bool IsSuccess, dynamic costFeebackResults)> PatchCostFeebackAsync(int id, CostFeedback costFeeback);

        Task<(bool IsSuccess, IEnumerable<CostFeedback> costFeebackResults)> GetCostFeeback(int scenarioId);
        Task<(bool IsSuccess, IEnumerable<CostOverView> costOverViewResults)> GetCostOverView(int scenarioId);
    }
}
