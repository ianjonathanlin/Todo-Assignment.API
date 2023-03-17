using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo_Assignment.API.Data.Entities;
using Todo_Assignment.API.Models;
using Todo_Assignment.API.Services;

namespace Todo_Assignment.API.Controllers
{
    [Route("api/[controller]")]
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
                _logger.LogInformation($"Action: GetAllTasks", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<TaskModel>>> AddTask(TaskModel task)
        {
            try
            {
                _taskRepository.AddTask(task);
                await _taskRepository.SaveChangesAsync();

                return Ok(await _taskRepository.GetTasksAsync());
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Action: AddTask", ex);
                return BadRequest();
            }
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> UpdateTask(int taskId, TaskModel task)
        {
            try
            {
                _taskRepository.UpdateTask(taskId, task);
                await _taskRepository.SaveChangesAsync();

                return Ok(await _taskRepository.GetTasksAsync());
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogInformation($"Task with ID {taskId} was not found. Action: UpdateTask", ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Action: UpdateTask", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> DeleteTask(int taskId)
        {
            try
            {
                _taskRepository.DeleteTask(taskId);
                await _taskRepository.SaveChangesAsync();

                return Ok(await _taskRepository.GetTasksAsync());
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogInformation($"Task with ID {taskId} was not found. Action: DeleteTask", ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Action: DeleteTask", ex);
                return BadRequest();
            }
        }
    }
}
