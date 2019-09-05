using Interfaces.BusinessLogic.Entities;
using Interfaces.Repository;
using System.Collections.Generic;

namespace Persistence.Repository
{

    public class TaskRepository : Interfaces.Repository.ITaskRepository
    {
    
        private readonly TaskContext _Context;
        
        public TaskRepository(TaskContext context)
        {
            _Context = context;
        }

        public virtual void Add(ITask task)
        {
            _Context.Add(task);          
        }

        public virtual void Edit(ITask task)
        {
             _Context.Edit(task);
        }

        public IEnumerable<ITask> GetAll(string sCode)
        {
            return _Context.ToList(sCode);
        }


    }

}
