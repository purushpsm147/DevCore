using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface ITaskExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Models.Task>>> GetTaskAsync();
    }
}
