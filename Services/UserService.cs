using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Entities;
using BackendApi.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BackendApi.Services
{
    public class UserService : IUserService
    {
        readonly UserContext _context;
        private readonly AppSettings _appSettings;
        public UserService(UserContext context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async System.Threading.Tasks.Task<User> Authenticate(string username, string password)
        {
            var user = await System.Threading.Tasks.Task.Run(() => _context.Users.Where(u => u.Email == username && u.Password == password).Include(x => x.Projects).Include("Tasks.Tags").FirstOrDefault());
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            await SaveUserData(user);
            return user;
        }

        public async System.Threading.Tasks.Task<IEnumerable<User>> GetAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<User>>(() =>
                _context.Users.Select(u => new User() { UserId = u.UserId, Username = u.Username, Email = u.Email, Password = u.Password, Created = u.Created, Role = u.Role, Token = u.Token, Tasks = u.Tasks, Projects = u.Projects}));
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
                    user.Role = user.Role ?? "User";
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
                    usr.Token = user.Token ?? usr.Token;
                    usr.Role = user.Role ?? usr.Role;
                    usr.Projects = user.Projects ?? usr.Projects;
                    _context.SaveChanges();
                
                    return true;
                }
                else
                    return false;
            });
        }

        
    }
}
