using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class LoadsClusterService : ILoadsClusterService
    {
        private readonly ExternalServices.ILoadsClusterExternalService _loadsClusterExternalService;
        public LoadsClusterService(ExternalServices.ILoadsClusterExternalService loadsClusterExternalService)
        {
            _loadsClusterExternalService = loadsClusterExternalService;
        }

        public async Task<(bool IsSuccess, IEnumerable<LoadsCluster> loadsClusterResult)> GetLoadsClusterAsync()
        {
            var loadsClusterResult = await _loadsClusterExternalService.GetLoadsClusterAsync();
            if (loadsClusterResult.IsSuccess)
            {
                
                return (true, loadsClusterResult.ResponseData);
            }

            return (false, null);
        }             
    }
}
