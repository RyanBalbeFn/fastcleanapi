using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<TaskModel> tasks = new List<TaskModel>
        {
            new TaskModel { Id = 1, Name = "Learn C#", IsCompleted = false },
            new TaskModel { Id = 2, Name = "Build an API", IsCompleted = false }
        };

        // GET: api/task (Get all tasks)
        [HttpGet]
        public ActionResult<IEnumerable<TaskModel>> GetTasks()
        {
            return Ok(tasks);
        }

        // GET: api/task/1 (Get a specific task)
        [HttpGet("{id}")]
        public ActionResult<TaskModel> GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found.");
            return Ok(task);
        }

        // POST: api/task (Create a new task)
        [HttpPost]
        public ActionResult<TaskModel> CreateTask([FromBody] TaskModel newTask)
        {
            if (newTask == null || string.IsNullOrEmpty(newTask.Name))
                return BadRequest("Task name is required.");

            newTask.Id = tasks.Count + 1;
            tasks.Add(newTask);
            return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask);
        }

        // PUT: api/task/1 (Update an existing task)
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found.");

            task.Name = updatedTask.Name;
            task.IsCompleted = updatedTask.IsCompleted;
            return NoContent();
        }

        // DELETE: api/task/1 (Delete a task)
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found.");

            tasks.Remove(task);
            return NoContent();
        }
    }
}
