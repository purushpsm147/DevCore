using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ITowerSupplierRegionService
    {
        Task<(bool IsSuccess, IEnumerable<TowerSupplierRegion> towerSupplierRegionResult)> GetTowerSupplierRegionAsync(int scenarioId);
    }
}
