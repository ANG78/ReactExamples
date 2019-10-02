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
        readonly ITaskToggleStatusValidationService _taskServices;

        public ValidateToggleStatus(ITaskToggleStatusValidationService taskService)
        {
            _taskServices = taskService;
        }

        public override string Description()
        {
            return "Validate Status Transitions when editing a task";
        }

        protected override IResult ExecuteTemplate(IManagementModelRequest<ITask> obj)
        {
            if (obj.Type == EnumOperation.EDITION)
            {
                return _taskServices.Validation(obj.Item);
            }

            return Result.Ok;
        }
    }

}


