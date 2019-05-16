using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services
{
    public class UserService : IUserService
    {
        readonly UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<User> Authenticate(string username, string password)
        {
            var user = await System.Threading.Tasks.Task.Run(() => _context.Users.Where(u => u.Email == username && u.Password == password).Include(x => x.Projects).Include("Tasks.Tags").FirstOrDefault());
            return user;    // user with passed credentials existe and get returned or NULL
        }

        public async System.Threading.Tasks.Task<IEnumerable<User>> GetAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<User>>(() =>
                _context.Users.Select(u => new User() { UserId = u.UserId, Username = u.Username, Email = u.Email, Password = u.Password, Created = u.Created, Active = u.Active, Tasks = u.Tasks, Projects = u.Projects}));
        }

        public async System.Threading.Tasks.Task<User> GetUser(long id)
        {
            return await System.Threading.Tasks.Task.Run<User>(() =>
                _context.Users.Where(u => u.UserId == id).Include(x => x.Projects).Include("Tasks.Tags").FirstOrDefault());
        }

        public async System.Threading.Tasks.Task<User> Register(User user)
        {
            return await System.Threading.Tasks.Task.Run<User>(() =>
            {
                User usr = _context.Users.SingleOrDefault(u => u.Email == user.Email);
                if (usr == null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return user;
                }
                else
                    return null;
            });
        }

        public async System.Threading.Tasks.Task<bool> SaveUserData(User user)
        {
            return await System.Threading.Tasks.Task.Run<bool>(() =>
            {
                User usr = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (usr != null)
                {
                    usr.Username = user.Username ?? usr.Username;
                    usr.Email = user.Email ?? usr.Email;
                    usr.Password = user.Password ?? usr.Password;
                    _context.SaveChanges();
                
                    return true;
                }
                else
                    return false;
            });
        }

        
    }
}
