using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Entities;

namespace BackendApi.Services
{
    public class ProjectService : IProjectService
    {

        readonly UserContext _context;
        public ProjectService(UserContext context)
        {
            _context = context;
        }


        public async Task<Project> AddProject(Project project)
        {
            return await System.Threading.Tasks.Task.Run<Project>(() =>
            {
                Project prj = _context.Projects.SingleOrDefault(p => p.ProjectId == project.ProjectId);
                if (prj == null)
                {
                    _context.Projects.Add(project);
                    _context.SaveChanges();
                    return project;
                }
                else
                    return null;
            });
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<Project>>(() =>
                _context.Projects.Select(p => new Project() { ProjectId = p.ProjectId, Title = p.Title, Running = p.Running, UserID = p.UserID, Tasks = p.Tasks }));
        }

        public async Task<Project> GetProject(long id)
        {
            return await System.Threading.Tasks.Task.Run<Project>(() =>
            {
                Project project = _context.Projects.SingleOrDefault(p => p.ProjectId == id);
                if(project == null)
                {
                    return null;
                }
                else
                {
                    return new Project() { ProjectId = project.ProjectId, Title = project.Title, Running = project.Running, UserID = project.UserID, Tasks = project.Tasks };
                }
            });
                
        }

        private Task<Project> NotFound(string v)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveProjectData(Project project)
        {
            return await System.Threading.Tasks.Task.Run<bool>(() =>
            {
                Project prj = _context.Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);
                if (prj != null)
                {
                    prj.Title = project.Title;
                    prj.Company = project.Company;
                    prj.Running = project.Running;
                    prj.UserID = project.UserID;
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            });
        }
    }
}
