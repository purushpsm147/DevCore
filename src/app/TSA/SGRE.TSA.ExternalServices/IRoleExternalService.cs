using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IRoleExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Role>>> GetRoleAsync();
        Task<ExternalServiceResponse<IEnumerable<PresetRoles>>> GetPresetRolesAsync();
    }
}
