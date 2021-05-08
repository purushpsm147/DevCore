using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IBaseTowerService
    {
        Task<(bool IsSuccess, dynamic baseTowerResult)> PutBaseTowerAsync(BaseTower baseTower);
        Task<(bool IsSuccess, IEnumerable<BaseTower> baseTowerResults)> GetBaseTowerAsync(int id);

        Task<(bool IsSuccess, dynamic baseTowerResult)> PatchBaseTowerAsync(int patchId, BaseTower baseTower);
    }
}
