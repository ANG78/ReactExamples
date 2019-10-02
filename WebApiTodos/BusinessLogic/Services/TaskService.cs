using BusinessLogic.Common;
using BusinessLogic.Services.Generics;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.Repository;

namespace BusinessLogic.Services
{

    public class TaskService : GenericService<ITask>, ITaskService
    { 
        public TaskService(ITaskRepository workerRepository) : base(workerRepository)
        {
        }

    }
}
