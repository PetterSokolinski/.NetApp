using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Entities;
using BackendApi.Services;
using Microsoft.EntityFrameworkCore;
using Task = BackendApi.Entities.Task;

namespace BackendApi.Services
{
    public class TaskService : ITaskService
    {
        readonly UserContext _context;
        public TaskService(UserContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task<IEnumerable<Entities.Task>> GetAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<Task>>(() =>
                _context.Tasks.Select(t => new Task() { TaskId = t.TaskId, Title = t.Title, ToStart = t.ToStart, ToFinish = t.ToFinish, Finished = t.Finished, Tags = t.Tags, Description = t.Description, UserID = t.UserID, ProjectID = t.ProjectID }));
        }


        public async System.Threading.Tasks.Task<Entities.Task> AddTask(Entities.Task task)
        {
            return await System.Threading.Tasks.Task.Run<Entities.Task>(() =>
            {
                Task tsk = _context.Tasks.SingleOrDefault(t => t.TaskId == task.TaskId);
                if (tsk == null)
                {                  
                    _context.Tasks.Add(task);
                    _context.SaveChanges();
                    return task;
                }
                else
                    return null;
            });
        }

        public async System.Threading.Tasks.Task<Entities.Task> GetTask(long id)
        {
            return await System.Threading.Tasks.Task.Run<Entities.Task>(() =>
                _context.Tasks.Where(t => t.TaskId == id).Include(x => x.Tags).FirstOrDefault());
        }

        public async System.Threading.Tasks.Task<bool> SaveTaskData(Entities.Task task)
        {
            return await System.Threading.Tasks.Task.Run<bool>(() =>
            {
                Entities.Task tsk = _context.Tasks.FirstOrDefault(t => t.TaskId == task.TaskId);
                if (tsk != null)
                {
                    tsk.Title = task.Title ?? tsk.Title;
                    tsk.ToStart = task.ToStart ?? tsk.ToStart;
                    tsk.ToFinish = task.ToFinish ?? tsk.ToFinish;
                    tsk.Finished = task.Finished;
                    tsk.Tags = task.Tags ?? tsk.Tags;
                    tsk.Description = task.Description ?? tsk.Description;
                    tsk.UserID = task.UserID ?? tsk.UserID;
                    tsk.ProjectID = task.ProjectID ?? tsk.ProjectID;
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            });
        }
        public async System.Threading.Tasks.Task<bool> RemoveTask(Entities.Task task)
        {
            return await System.Threading.Tasks.Task.Run<bool>(() =>
            {
                _context.Remove(task);
                _context.SaveChanges();
                return true;
            });
        }



    }
}
