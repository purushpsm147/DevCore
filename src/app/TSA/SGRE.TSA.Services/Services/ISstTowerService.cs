using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ISstTowerService
    {
        Task<(bool IsSuccess, IEnumerable<SstTower> sstResults)> GetSstTowerIdAsync(int id);
        Task<(bool IsSuccess, IEnumerable<SstTower> sstResults)> GetSstTowerIdNoExpandAsync(int id);
        Task<(bool IsSuccess, dynamic sstResults)> PutSstTowerAsync(SstTower sstInput);
        Task<(bool IsSuccess, dynamic sstResults)> PatchSstTowerAsync(int patchId, SstTower sstInput);
        Task<(bool IsSuccess, IEnumerable<InitialTower> sstResults)> GetIntialTowerAsync(int WtgCatalogueid, int WtgCatalogueModelId, decimal ProposedHubHeight);
        Task<(bool IsSuccess, dynamic sstResults)> PatchSstTowerWithoutCostKpiAsync(int patchId, SstTower sstInput);
    }
}