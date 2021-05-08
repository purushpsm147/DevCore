using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IPermissionExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Permission>>> GetPermissionAsync(int roleId);
    }
}
