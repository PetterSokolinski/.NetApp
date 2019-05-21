using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Entities;
using BackendApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;
        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        // GET: api/Projects
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _service.GetAll();
            return Ok(projects);
        }

        // POST: api/Projects
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> PostProject([FromBody] Project project)
        {
            if (project == null)
            {
                BadRequest();
            }
            var prj = await _service.AddProject(project);
            if (prj != null)
            {
                return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
            }
            else
                return Conflict();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Project>> GetProject([FromRoute] int id)
        {
            var task = await _service.GetProject(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> PutTask([FromRoute] int id, [FromBody] Project project)
        {
            if (project == null || id != project.ProjectId)
                return BadRequest();

            var prj = await _service.GetProject(id);
            if (prj == null)
                return NotFound("User does not exist");

            if (await _service.SaveProjectData(project))
                return NoContent();
            else
                return NotFound("User does not exist");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _service.GetProject(id);

            if (project == null)
            {
                return NotFound("User does not exist");
            }
            await _service.RemoveProject(project);
            return NoContent();

        }


    }
}
