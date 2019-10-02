using BusinessLogic.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using System.Linq;

namespace BusinessLogic.Services
{
    public class TaskToggleStatusValidationService : ITaskToggleStatusValidationService
    {
        readonly ITaskService _taskServices;
        public TaskToggleStatusValidationService(ITaskService taskServices)
        {
            _taskServices = taskServices;
        }

        public IResult Validation(ITask task)
        {
            var result = _taskServices.GetAll(task.Code);
            var resTasksFound = result.ToList();
            var taskSaved = result.ToList().FirstOrDefault();

            if (taskSaved == null || resTasksFound.Count != 1)
            {
                return new Result(EnumResultBL.ERROR_CODE_NOT_EXIST, task.Code);
            }

            if (task.Status == EnumStatusTask.Completed &&
                taskSaved.Status == EnumStatusTask.Pending)
            {
                return Result.Ok;
            }
            else if (task.Status == EnumStatusTask.Pending &&
                        taskSaved.Status == EnumStatusTask.Completed)
            {
                return Result.Ok;
            }

            return Result.Ok;
        }
    }
}
