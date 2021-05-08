using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ExternalServices.ITaskExternalService taskService;
        public TaskService(ExternalServices.ITaskExternalService taskService)
        {
            this.taskService = taskService;
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Task> taskResult)> GetTaskAsync()
        {
            var taskResult = await taskService.GetTaskAsync();
            if (taskResult.IsSuccess)
            {
                return (true, taskResult.ResponseData);
            }

            return (false, null);
        }
    }
}
