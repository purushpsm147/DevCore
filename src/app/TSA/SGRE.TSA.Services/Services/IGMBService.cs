using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IGMBService
    {
        Task<(bool IsSuccess, IEnumerable<GenericMarketBoundary> genericMarketBoundaries)> GetGMBAsync(int CountryId);
    }
}
