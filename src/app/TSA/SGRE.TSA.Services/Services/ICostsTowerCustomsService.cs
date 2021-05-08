using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsTowerCustomsService
    {
        Task<(bool IsSuccess, IEnumerable<CostsTowerCustomsMeta> costsTowerCustomsMetaResult)> GetCostsTowerCustomsMetaAsync();
        Task<(bool IsSuccess, IEnumerable<CostsTowerCustomsMeta> costsTowerCustomsMetaResult)> GetCostsTowerCustomsMetaAsync(int id);
    }
}
