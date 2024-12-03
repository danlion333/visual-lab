using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualLabAPI.Data;
using VirtualLabAPI.Models;

namespace VirtualLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksInProgressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksInProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TasksInProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskInProgress>>> GetTasksInProgress()
        {
            return await _context.TasksInProgress
                .Include(tip => tip.User)
                .Include(tip => tip.Task)
                .ToListAsync();
        }

        // GET: api/TasksInProgress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskInProgress>> GetTaskInProgress(int id)
        {
            var taskInProgress = await _context.TasksInProgress
                .Include(tip => tip.User)
                .Include(tip => tip.Task)
                .FirstOrDefaultAsync(tip => tip.Id == id);

            if (taskInProgress == null)
            {
                return NotFound();
            }

            return taskInProgress;
        }

        // POST: api/TasksInProgress
        [HttpPost]
        public async Task<ActionResult<TaskInProgress>> PostTaskInProgress(TaskInProgress taskInProgress)
        {
            _context.TasksInProgress.Add(taskInProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskInProgress), new { id = taskInProgress.Id }, taskInProgress);
        }

        // PUT: api/TasksInProgress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskInProgress(int id, TaskInProgress taskInProgress)
        {
            if (id != taskInProgress.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskInProgress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskInProgressExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/TasksInProgress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskInProgress(int id)
        {
            var taskInProgress = await _context.TasksInProgress.FindAsync(id);
            if (taskInProgress == null)
            {
                return NotFound();
            }

            _context.TasksInProgress.Remove(taskInProgress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskInProgressExists(int id)
        {
            return _context.TasksInProgress.Any(e => e.Id == id);
        }
    }
}
