using BusinessLogic.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Services
{

    public class TaskValidationService : ITaskValidationService
    {
        readonly int _maxCharactersTitle;
        readonly int _maxCharactersDesc;

        public TaskValidationService(int maxCharactersTitle, int maxCharactersDesc)
        {
            _maxCharactersTitle = maxCharactersTitle;
            _maxCharactersDesc = maxCharactersDesc;
        }

        public IResult Validation(ITask task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                return new Result(EnumResultBL.ERROR_TITLE_EMPTY);
            }

            if (!task.Status.IsOk())
            {
                return new Result(EnumResultBL.ERROR_VALID_STATUS_REQUIRED);
            }

            if (_maxCharactersTitle > 0 && task.Title.Trim().Length > _maxCharactersTitle)
            {
                return new Result(EnumResultBL.ERROR_VALID_MAX_TITLE, task.Title.Trim().Length);
            }

            if (_maxCharactersDesc > 0 && task.Description.Trim().Length > _maxCharactersDesc)
            {
                return new Result(EnumResultBL.ERROR_VALID_MAX_DEC, task.Description.Trim().Length);
            }

            return Result.Ok;
        }
    }
}
