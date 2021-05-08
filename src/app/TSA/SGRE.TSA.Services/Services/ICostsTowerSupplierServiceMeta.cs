using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICostsTowerSupplierServiceMeta
    {
        Task<(bool IsSuccess, IEnumerable<CostsTowerSupplierMeta> costsTowerSupplierResult)> GetCostsTowerSupplierAsync();
        Task<(bool IsSuccess, IEnumerable<CostsTowerSupplierMeta> costsTowerSupplierResult)> GetCostsTowerSupplierAsync(int id);
    }
}
