using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsTowerLogisticsLeadTimeService
    {
        Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsLeadTimeMeta> costsTowerLogisticsLeadTimeMetaResult)> GetCostsTowerLogisticsLeadTimeMetaAsync();
        Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsLeadTimeMeta> costsTowerLogisticsLeadTimeMetaResult)> GetCostsTowerLogisticsLeadTimeMetaAsync(int id);
    }
}
