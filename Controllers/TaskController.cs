using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Todo_Assignment.API.Data.DbContexts;
using Todo_Assignment.API.Data.Entities;
using Todo_Assignment.API.Models;

namespace Todo_Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;
        private readonly IMapper _mapper;

        public TaskController(TaskContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetAllTasks()
        {
            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                return Ok(_mapper.Map<IEnumerable<TaskModel>>(tasks));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }

        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskEntity>> GetTaskById(int taskId)
        {
            try
            {
                var task = await _context.Tasks.Where(t => t.Id == taskId).SingleOrDefaultAsync();
                if (task == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<TaskModel>(task));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskEntity>> CreateTask(TaskModel task)
        {
            try
            {
                var newTask = new TaskEntity
                {
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Category = task.Category,
                    CreatedDateTime = DateTime.UtcNow,
                    LatestUpdatedDateTime = DateTime.UtcNow
                };

                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<TaskModel>(newTask));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult<TaskEntity>> UpdateTask(int taskId, TaskModel task)
        {
            try
            {
                var existingTask = _mapper.Map<TaskEntity>(
                    await _context.Tasks.Where(t => t.Id == taskId).SingleOrDefaultAsync());

                if (existingTask == null)
                {
                    return NotFound();
                }

                var updatedTask = new TaskEntity
                {
                    Id = existingTask.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Category = task.Category,
                    CreatedDateTime = existingTask.CreatedDateTime,
                    LatestUpdatedDateTime = DateTime.UtcNow
                };

                // Update with new values & fields
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.Category = updatedTask.Category;
                existingTask.LatestUpdatedDateTime = updatedTask.LatestUpdatedDateTime;

                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<TaskModel>(existingTask));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult<TaskEntity>> DeleteTask(int taskId)
        {
            try
            {
                var taskToBeDeleted = await _context.Tasks.Where(t => t.Id == taskId).SingleOrDefaultAsync();
                if (taskToBeDeleted == null)
                {
                    return NotFound();
                }

                // Soft Delete
                taskToBeDeleted.IsDeleted = true;
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }
        }
    }
}
