using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackendApi.Entities;
using BackendApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IProjectService _projectService;
        public UsersController(IUserService service, IProjectService projectService)
        {
            _service = service;
            _projectService = projectService;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User user)
        {
            var usr = await _service.Authenticate(user.Email, user.Password);

            if (usr == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(usr);
        }

        // GET: api/Users
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [EnableCors("AllowAllHeaders")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [EnableCors("AllowAllHeaders")]
        public async Task<ActionResult<User>> GetUser([FromRoute] int id)
        {
            var user = await _service.GetUser(id);
            if (user == null)
                return NotFound();

           var currentUserId = int.Parse(User.Identity.Name);
           if (id != currentUserId && !User.IsInRole(Role.Admin))
           {
               return Forbid();
           }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [EnableCors("AllowAllHeaders")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (user == null || id != user.UserId)
                return BadRequest();

            var usr = await _service.GetUser(id);
            if (usr == null)
                return NotFound("User does not exist");

            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            if (await _service.SaveUserData(user))
                return NoContent();
            else
                return NotFound("User does not exist");
        }


        //[Route("addproject")]
        [HttpPost("addproject/{id:int}/{projectId:int}")]
        [EnableCors("AllowAllHeaders")]
        public async Task<IActionResult> AddProjectToTheUser(int id, int projectId)
        {
            User user = await _service.GetUser(id);
            Project project = await _projectService.GetProject(projectId);
            user.Projects.Add(new ProjectAndUser { Project = project });
            if (await _service.SaveUserData(user))
            {
                return NoContent();
            }
            else
            {
                return NotFound("User does not exist");
            }
        }

        [AllowAnonymous]
        // POST: api/Users
        [HttpPost]
        [EnableCors("AllowAllHeaders")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user == null)   
            {
                BadRequest();
            }
            var u = await _service.Register(user);
            if (u != null)
            {
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            }
            else
                return Conflict(); // HTTP-Status: 409
        }


    }
}
