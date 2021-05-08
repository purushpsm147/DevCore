using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostOverViewService
    {
        Task<(bool IsSuccess, IEnumerable<CostOverViewMeta> costOverViewMetaResult)> GetCostOverViewMetaAsync();
        Task<(bool IsSuccess, IEnumerable<CostOverViewMeta> costOverViewMetaResult)> GetCostOverViewMetaAsync(int id);
    }
}
