using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TaskModel>>(tasks));
        }

        [HttpGet("{taskid}")]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetTaskById(int taskId)
        {
            var task = await _context.Tasks.Where(t => t.Id == taskId).SingleOrDefaultAsync();

            return Ok(_mapper.Map<TaskModel>(task));
        }

        [HttpPost]
        public async Task<ActionResult<TaskEntity>> CreateTask(TaskModel task)
        {
            var newTask = _mapper.Map<TaskEntity>(task);

            // validation for model

            // fill in blanks e.g. createdDate, lastUpdated etc.

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<TaskModel>(newTask));
        }

        [HttpPut("{taskid}")]
        public async Task<ActionResult<TaskEntity>> UpdateTask(int taskId, TaskModel task)
        {
            var existingTask = await _context.Tasks.Where(t => t.Id == taskId).SingleOrDefaultAsync();

            // validation for model

            // fill in blanks e.g. createdDate, lastUpdated etc.

            if (existingTask != null)
            {
                var updatedTask = _mapper.Map<TaskEntity>(task);

                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.Category = updatedTask.Category;
                existingTask.LatestUpdatedDateTime = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<TaskModel>(existingTask));
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskEntity>> DeleteTask(int taskId)
        {
            var taskToBeDeleted = await _context.Tasks.Where(t => t.Id == taskId).SingleOrDefaultAsync();

            if (taskToBeDeleted != null)
            {
                // Soft Delete
                taskToBeDeleted.IsDeleted = true;
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }
    }
}
