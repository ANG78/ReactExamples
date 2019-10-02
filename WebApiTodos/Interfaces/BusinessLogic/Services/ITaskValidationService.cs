using Interfaces.BusinessLogic.Entities;

namespace Interfaces.BusinessLogic.Services
{
    public interface ITaskValidationService
    {
        IResult Validation(ITask task);
    }
    public interface ITaskToggleStatusValidationService : ITaskValidationService { }


}
