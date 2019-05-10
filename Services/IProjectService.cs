using BackendApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project> GetProject(long id);
        Task<Project> AddProject(Project project);
        Task<bool> SaveProjectData(Project project);
    }
}
