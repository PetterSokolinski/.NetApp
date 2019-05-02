using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Entities;

namespace BackendApi.Services
{
    public class UserService : IUserService
    {
        readonly UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await System.Threading.Tasks.Task.Run(() => _context.Users.SingleOrDefault(u => u.Email == username && u.Password == password));
            return user;    // user with passed credentials existe and get returned or NULL
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<User>>(() =>
                _context.Users.Select(u => new User() { Id = u.Id, Username = u.Username, Email = u.Email, Password = u.Password, Created = u.Created, Active = u.Active,  Tasks = u.Tasks}));
        }

        public async Task<User> GetUser(long id)
        {
            return await System.Threading.Tasks.Task.Run<User>(() =>
                _context.Users.SingleOrDefault(u => u.Id == id));
        }

        public async Task<User> Register(User user)
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

        public async Task<bool> SaveUserData(User user)
        {
            return await System.Threading.Tasks.Task.Run<bool>(() =>
            {
                User usr = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (usr != null)
                {
                    usr.Username = user.Username;
                    usr.Email = user.Email;
                    usr.Password = user.Password;
                    usr.Active = user.Active;
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            });
        }
    }
}
