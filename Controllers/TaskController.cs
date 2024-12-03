using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using VirtualLabAPI.Data;
using VirtualLabAPI.Models;

namespace VirtualLabAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public TasksController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/Tasks
		[HttpGet]
		public async Task<ActionResult<IEnumerable<VirtualLabAPI.Models.Task>>> GetTasks()
		{
			return await _context.Tasks.ToListAsync();
		}

		// GET: api/Tasks/5
		[HttpGet("{id}")]
		public async Task<ActionResult<VirtualLabAPI.Models.Task>> GetTask(int id)
		{
			var task = await _context.Tasks.FindAsync(id);

			if (task == null)
			{
				return NotFound();
			}

			return task;
		}

		// GET: api/Tasks/TaskForStudent/{taskId}
		[HttpGet("TaskForStudent/{taskId}")]
		public async Task<IActionResult> GetTaskForStudent(int taskId)
		{
			// Find the task by ID
			var task = await _context.Tasks.FindAsync(taskId);

			if (task == null)
			{
				return NotFound("Task not found.");
			}

			// Parse UML JSON and remove relationships
			var uml = JsonConvert.DeserializeObject<dynamic>(task.Uml);
			if (uml != null)
			{
				uml.relationships = null; // Remove relationships
			}

			// Create a new response object with modified UML
			var modifiedTask = new
			{
				task.Id,
				task.Title,
				task.Description,
				task.MaxPoints,
				Uml = uml // Modified UML
			};

			return Ok(modifiedTask);
		}

		// POST: api/Tasks
		[HttpPost]
		public async Task<ActionResult<VirtualLabAPI.Models.Task>> PostTask(VirtualLabAPI.Models.Task task)
		{
			_context.Tasks.Add(task);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
		}

		// PUT: api/Tasks/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTask(int id, VirtualLabAPI.Models.Task task)
		{
			if (id != task.Id)
			{
				return BadRequest();
			}

			_context.Entry(task).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TaskExists(id))
				{
					return NotFound();
				}
				throw;
			}

			return NoContent();
		}

		// DELETE: api/Tasks/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTask(int id)
		{
			var task = await _context.Tasks.FindAsync(id);
			if (task == null)
			{
				return NotFound();
			}

			_context.Tasks.Remove(task);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TaskExists(int id)
		{
			return _context.Tasks.Any(e => e.Id == id);
		}
	}
}
