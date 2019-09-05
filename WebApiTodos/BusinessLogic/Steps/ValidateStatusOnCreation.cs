using BusinessLogic.Common;
using BusinessLogic.Steps.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Steps
{
    public class ValidateStatusOnCreation : StepTemplateGeneric<IManagementModelRequest<ITask>>
    {
        readonly ITaskService _taskServices;

        public ValidateStatusOnCreation()
        {
        }

        public override string Description()
        {
            return "Validate status allowed";
        }

        protected override IResult ExecuteTemplate(IManagementModelRequest<ITask> obj)
        {
            if (obj.Type == EnumOperation.NEW)
            {
                if (obj.Item.Status == EnumStatusTask.Pending)
                {
                    return Result.Ok;
                }

                return new Result(EnumResultBL.ERROR_STATUS_NOT_ALLOWED_ON_CREATION, obj.Item.Status);

            }

            return Result.Ok;
        }
    }

}


