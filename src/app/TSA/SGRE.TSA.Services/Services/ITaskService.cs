using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ITaskService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Task> taskResult)> GetTaskAsync();
    }
}
