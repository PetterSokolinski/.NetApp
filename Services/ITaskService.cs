using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Controllers;
using BackendApi.Entities;

namespace BackendApi.Services
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task<IEnumerable<Entities.Task>> GetAll();
        System.Threading.Tasks.Task<Entities.Task> AddTask(Entities.Task task);
        System.Threading.Tasks.Task<Entities.Task> GetTask(long id);
        System.Threading.Tasks.Task<bool> SaveTaskData(Entities.Task task);
        System.Threading.Tasks.Task<bool> RemoveTask(Entities.Task task);

    }
}
