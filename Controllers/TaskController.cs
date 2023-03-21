using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo_Assignment.API.Models;
using Todo_Assignment.API.Services;

namespace Todo_Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger _logger;

        public TaskController(ITaskRepository taskRepository, ILogger<TaskController> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks()
        {
            try
            {
                return Ok(await _taskRepository.GetTasksAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: GetAllTasks");
                return BadRequest("Unable to retrieve tasks.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<TaskModel>>> AddTask(TaskModel task)
        {
            try
            {
                _taskRepository.AddTask(task);
                await _taskRepository.SaveChangesAsync();

                return NoContent(); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: AddTask");
                return BadRequest("Unable to add task.");
            }
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> UpdateTask(int taskId, TaskModel task)
        {
            try
            {
                _taskRepository.UpdateTask(taskId, task);
                await _taskRepository.SaveChangesAsync();

                return NoContent();
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogError(ex, $"Task with ID {taskId} was not found. Action: UpdateTask", taskId);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: UpdateTask");
                return BadRequest("Unable to edit task");
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> DeleteTask(int taskId)
        {
            try
            {
                _taskRepository.DeleteTask(taskId);
                await _taskRepository.SaveChangesAsync();

                return NoContent();
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogError(ex, $"Task with ID {taskId} was not found. Action: DeleteTask", taskId);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: DeleteTask");
                return BadRequest("Unable to delete task.");
            }
        }
    }
}
