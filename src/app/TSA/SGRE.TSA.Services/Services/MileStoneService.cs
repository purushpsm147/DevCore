using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class MileStoneService : IMileStoneService
    {
        private readonly ExternalServices.IMileStonesExternalService mileStoneService;
        public MileStoneService(ExternalServices.IMileStonesExternalService mileStoneService)
        {
            this.mileStoneService = mileStoneService;
        }

        public async Task<(bool IsSuccess, IEnumerable<MileStone> mileStoneResult)> GetMileStoneAsync()
        {
            var mileStoneResult = await mileStoneService.GetMileStonesAsync();
            if (mileStoneResult.IsSuccess)
            {
                return (true, mileStoneResult.ResponseData);
            }

            return (false, null);
        }

    }
}
