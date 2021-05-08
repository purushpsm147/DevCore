using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ILoadsClusterService
    {
        Task<(bool IsSuccess, IEnumerable<LoadsCluster> loadsClusterResult)> GetLoadsClusterAsync();
    }
}
