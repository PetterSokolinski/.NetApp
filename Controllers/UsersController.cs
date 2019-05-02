using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackendApi.Entities;
using BackendApi.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        //// GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] int id)
        {
            var user = await _service.GetUser(id);
            if (user == null)
                return NotFound();

            if (user.Email != User.Identity.Name) // use the created identity 
                return Forbid();

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (user == null || id != user.Id)
                return BadRequest();

            var usr = await _service.GetUser(id);
            if (usr == null)
                return NotFound("User does not exist");

            if (usr.Email != User.Identity.Name)
                return Forbid();

            if (await _service.SaveUserData(user))
                return NoContent();
            else
                return NotFound("User does not exist");
        }
        [AllowAnonymous]
        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                BadRequest();
            }
            var u = await _service.Register(user);
            if (u != null)
            {
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            else
                return Conflict(); // HTTP-Status: 409
        }
    }
}
