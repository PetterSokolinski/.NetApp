using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Entities;

namespace BackendApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUser(long id);
        Task<User> Register(User user);
        Task<bool> SaveUserData(User user);
    }
}
