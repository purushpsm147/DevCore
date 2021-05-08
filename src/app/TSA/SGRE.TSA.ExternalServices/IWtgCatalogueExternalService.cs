using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IWtgCatalogueExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<WtgCatalogue>>> GetWtgCatalogueAsync();
        Task<ExternalServiceResponse<WtgCatalogue>> GetWtgCatalogueAsync(int id);
        Task<ExternalServiceResponse<WtgThreshold>> GetWtgThresholdAsync();
    }
}
