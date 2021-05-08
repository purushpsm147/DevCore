using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IMileStonesExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<MileStone>>> GetMileStonesAsync();
    }
}
