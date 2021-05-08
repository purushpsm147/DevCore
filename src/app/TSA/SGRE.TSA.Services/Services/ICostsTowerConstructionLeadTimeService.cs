using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsTowerConstructionLeadTimeService
    {
        Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionLeadTimeMeta> costsTowerConstructionLeadTimeMetaResult)> GetCostsTowerConstructionLeadTimeMetaAsync();
        Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionLeadTimeMeta> costsTowerConstructionLeadTimeMetaResult)> GetCostsTowerConstructionLeadTimeMetaAsync(int id);
    }
}