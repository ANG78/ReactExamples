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
        readonly ITaskService _taskRepository;
        readonly int _maxCharactersTitle;
        readonly int _maxCharactersDesc;
        public ValidateAndCompleteTask( int maxCharactersTitle, int maxCharactersDesc)
        {
            _maxCharactersTitle = maxCharactersTitle;
            _maxCharactersDesc = maxCharactersDesc;
        }

        public override string Description()
        {
            return "Check and Validate Task";
        }

        protected override IResult ExecuteTemplate(IManagementModelRequest<ITask> obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Item.Title))
            {
                return new Result(EnumResultBL.ERROR_TITLE_EMPTY);
            }

            if (!obj.Item.Status.IsOk())
            {
                return new Result(EnumResultBL.ERROR_VALID_STATUS_REQUIRED);
            }

            if (_maxCharactersTitle > 0 && obj.Item.Title.Trim().Length > _maxCharactersTitle)
            {
                return new Result(EnumResultBL.ERROR_VALID_MAX_TITLE, obj.Item.Title.Trim().Length);
            }

            if (_maxCharactersDesc > 0 && obj.Item.Description.Trim().Length > _maxCharactersDesc)
            {
                return new Result(EnumResultBL.ERROR_VALID_MAX_DEC, obj.Item.Description.Trim().Length);
            }

            return Result.Ok;
        }
    }

}


