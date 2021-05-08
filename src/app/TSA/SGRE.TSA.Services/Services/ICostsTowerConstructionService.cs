using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsTowerConstructionService
    {
        Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionMeta> costsTowerConstructionMetaResult)> GetCostsTowerConstructionMetaAsync();
        Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionMeta> costsTowerConstructionMetaResult)> GetCostsTowerConstructionMetaAsync(int id);
    }
}
