using BusinessLogic.Common;
using BusinessLogic.Steps.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;
using System.Linq;

namespace BusinessLogic.Steps
{
    public class ValidateToggleStatus : StepTemplateGeneric<IManagementModelRequest<ITask>>
    {
        readonly ITaskService _taskServices;

        public ValidateToggleStatus(ITaskService taskService)
        {
            _taskServices = taskService;
        }

        public override string Description()
        {
            return "Validate Status Transition on editing operations";
        }

        protected override IResult ExecuteTemplate(IManagementModelRequest<ITask> obj)
        {
            if (obj.Type == EnumOperation.EDITION)
            {
                var result = _taskServices.GetAll(obj.Item.Code);
                var resTasksFound = result.ToList();
                var taskSaved = result.ToList().FirstOrDefault();

                if (taskSaved == null || resTasksFound.Count != 1)
                {
                    return new Result(EnumResultBL.ERROR_CODE_NOT_EXIST, obj.Item.Code);
                }

                if (obj.Item.Status == EnumStatusTask.Completed &&
                    taskSaved.Status == EnumStatusTask.Pending)
                {
                    return Result.Ok;
                }
                else if (obj.Item.Status == EnumStatusTask.Pending &&
                         taskSaved.Status == EnumStatusTask.Completed)
                {
                    return Result.Ok;
                }
                
            }

            return Result.Ok;
        }
    }

}


