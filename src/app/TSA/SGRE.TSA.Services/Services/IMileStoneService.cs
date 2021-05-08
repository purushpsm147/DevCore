using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IMileStoneService
    {
        Task<(bool IsSuccess, IEnumerable<MileStone> mileStoneResult)> GetMileStoneAsync();

    }
}
