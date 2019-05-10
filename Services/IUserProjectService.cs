using BackendApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Services
{
    interface IUserProjectService
    {
        Task<IEnumerable<UserProject>> GetAll();
        Task<UserProject> GetProjectAndUsers(long id);
        Task<UserProject> AddProjectAndUsers(UserProject project);
        Task<bool> SaveProjectAndUsersData(UserProject project);
    }
}
