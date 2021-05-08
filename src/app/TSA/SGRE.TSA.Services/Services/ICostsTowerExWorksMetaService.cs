using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsTowerExWorksMetaService
    {
        Task<(bool IsSuccess, IEnumerable<CostsTowerExWorksMeta> costsTowerExWorksResult)> GetCostsTowerExWorksAsync();
        Task<(bool IsSuccess, IEnumerable<CostsTowerExWorksMeta> costsTowerExWorksResult)> GetCostsTowerExWorksAsync(int id);
    }
}
