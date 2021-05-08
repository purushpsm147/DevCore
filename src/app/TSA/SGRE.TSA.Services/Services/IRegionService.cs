using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IRegionService
    {
        Task<(bool IsSuccess, IEnumerable<Region> regionResult)> GetRegionAsync();
        Task<(bool IsSuccess, dynamic regionResult)> GetRegionAsync(int id);
    }
}
