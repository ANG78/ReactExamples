using Interfaces.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Repository
{
    public class TaskContext
    {
        private static int _internalId = DateTime.Now.Millisecond;
        private static readonly object Locker = new object();

        private IList<ITask> _CurrentTask = new List<ITask>();

        public TaskContext()
        {
        }

        public int GenerateId()
        {
            lock (Locker)
            {
                _internalId = _internalId + 1;
                return _internalId;
            }
        }

        public void Add(ITask item)
        {
            if (string.IsNullOrWhiteSpace(item.Title) ||
                !item.Status.IsOk() ||
                string.IsNullOrWhiteSpace(item.Code)
                )
            {
                throw new Exception("Title/Status are required");
            }

            item.Title = item.Title.Trim();
            string sCode = item.Code.Trim();

            var found = _CurrentTask.FirstOrDefault(x => x.Code == sCode);
            if (found == null)
            {
                item.Id = GenerateId();
                _CurrentTask.Add(item);
            }
            else
            {
                throw new Exception("There is already a task with this Title.");
            }


        }

        public void Edit(ITask item)
        {

            if (string.IsNullOrWhiteSpace(item.Title) ||
                !item.Status.IsOk() ||
                string.IsNullOrWhiteSpace(item.Code)
                )
            {
                throw new Exception("Code/Status are required");
            }

            item.Title = item.Title.Trim();

            lock (Locker)
            {
                if (item.Id > 0)
                {
                    var task = _CurrentTask.FirstOrDefault(x => x.Id == item.Id);
                    if (task != null)
                    {
                        _CurrentTask.Remove(task);
                    }
                    _CurrentTask.Add(item);
                }
            }
        }

        public ITask GetObjectById(int id)
        {
            lock (Locker)
            {
                var originalTask = _CurrentTask.FirstOrDefault(x => x.Id == id);
                if (originalTask == null)
                {
                    throw new Exception("Not found Task with id " + id);
                }
                return originalTask;
            }

        }

        public IEnumerable<ITask> ToList(string sCode)
        {
            sCode = sCode?.Trim();

            lock (Locker)
            {
                if (string.IsNullOrWhiteSpace(sCode))
                {
                    return _CurrentTask.ToList();
                }
                else
                {
                    return _CurrentTask.Where(x => x.Code == sCode).ToList();
                }
            }
        }

    }

}
