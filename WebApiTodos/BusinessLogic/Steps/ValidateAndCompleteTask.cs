using BusinessLogic.Common;
using BusinessLogic.Steps.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Steps
{

    public class ValidateAndCompleteTask : StepTemplateGeneric<IManagementModelRequest<ITask>>
    {
        readonly ITaskValidationService _validationService;

        public ValidateAndCompleteTask(ITaskValidationService validationService)
        {
            _validationService = validationService;
        }

        public override string Description()
        {
            return "Check and Validate Task";
        }

        protected override IResult ExecuteTemplate(IManagementModelRequest<ITask> obj)
        {
            return _validationService.Validation(obj.Item);
        }
    }

}


