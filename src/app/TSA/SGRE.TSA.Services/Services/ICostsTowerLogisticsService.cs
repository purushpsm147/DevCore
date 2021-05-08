using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsTowerLogisticsService
    {
        Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsMeta> costsTowerLogisticsMetaResult)> GetCostsTowerLogisticsMetaAsync();
        Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsMeta> costsTowerLogisticsMetaResult)> GetCostsTowerLogisticsMetaAsync(int id);
    }
}
