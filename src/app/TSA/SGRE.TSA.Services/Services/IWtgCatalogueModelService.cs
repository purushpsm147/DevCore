using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IWtgCatalogueModelService
    {
        Task<(bool IsSuccess, IEnumerable<WtgCatalogueModel> wtgCatalogueModelResult)> GetWtgCatalogueModelBywtgCatalougeIdAsync(int wtgCatalougeId);
    }
}
