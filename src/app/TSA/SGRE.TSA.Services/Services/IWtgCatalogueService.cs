using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IWtgCatalogueService
    {
        Task<(bool IsSuccess, IEnumerable<WtgCatalogue> wtgCatalogueResult)> GetWtgCatalogueAsync();
        Task<(bool IsSuccess, WtgCatalogue wtgCatalogueResult)> GetWtgCatalogueAsync(int id);
        Task<(bool IsSuccess, WtgThreshold wtgThresholdResult)> GetWtgThresholdAsync();
    }
}
