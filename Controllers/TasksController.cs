﻿using System;
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
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;
        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/Tasks
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<Entities.Task>>> GetTasks()
        {
            var tasks = await _service.GetAll();
            return Ok(tasks);
        }

        // POST: api/Tasks
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> PostTask([FromBody] Entities.Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            var tsk = await _service.AddTask(task);
            if (tsk != null)
            {
                return CreatedAtAction("GetTask", new { id = task.TaskId }, task);
            }
            else
                return Conflict();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Entities.Task>> GetTask([FromRoute] int id)
        {
            var task = await _service.GetTask(id);
            if (task == null)
                return NotFound();
      
            return Ok(task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> PutTask([FromRoute] int id, [FromBody] Entities.Task task)
        {
            if (task == null || id != task.TaskId)
                return BadRequest();

            var tsk = await _service.GetTask(id);
            if (tsk == null)
                return NotFound("Task does not exist");

            if (await _service.SaveTaskData(task))
                return NoContent();
            else
                return NotFound("Task does not exist");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(long id)
        {
            var task = await _service.GetTask(id);

            if (task == null)
            {
                return NotFound("Task does not exist");
            }
            await _service.RemoveTask(task);
            return NoContent();

        }

    }
}
