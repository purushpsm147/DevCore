using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IApplicationReasonService
    {
        Task<(bool IsSuccess, IEnumerable<ApplicationReason> ApplicationReasonResult)> GetApplicationReasonAsync();
    }
}
