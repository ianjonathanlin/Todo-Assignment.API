﻿using AutoMapper;
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
        private readonly ILogger _logger;

        public TaskController(TaskContext context, IMapper mapper, ILogger<TaskController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetAllTasks()
        {
            try
            {
                throw new Exception("testing");

                var tasks = await _context.Tasks.ToListAsync();
                return Ok(_mapper.Map<IEnumerable<TaskModel>>(tasks));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception occured in GetAllTasks.", ex);
                return StatusCode(500, "Server Failure.");
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
                    _logger.LogInformation($"Task with ID {taskId} was not found. Action: GetTaskById");
                    return NotFound();
                }

                return Ok(_mapper.Map<TaskModel>(task));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception occured in GetTaskById.", ex);
                return StatusCode(500, "Server Failure.");
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
                _logger.LogCritical("Exception occured in CreateTask.", ex);
                return StatusCode(500, "Server Failure.");
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
                    _logger.LogInformation($"Task with ID {taskId} was not found. Action: UpdateTask");
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
            catch (Exception ex)
            {
                _logger.LogCritical("Exception occured in UpdateTask.", ex);
                return StatusCode(500, "Server Failure.");
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
                    _logger.LogInformation($"Task with ID {taskId} was not found. Action: DeleteTask");
                    return NotFound();
                }

                // Soft Delete
                taskToBeDeleted.IsDeleted = true;
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception occured in DeleteTask.", ex);
                return StatusCode(500, "Server Failure.");
            }
        }
    }
}
