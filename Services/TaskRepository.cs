using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo_Assignment.API.Data.DbContexts;
using Todo_Assignment.API.Data.Entities;
using Todo_Assignment.API.Models;

namespace Todo_Assignment.API.Services
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoDbContext _context;
        private readonly IMapper _mapper;

        public TaskRepository(TodoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync()
        {
            return _mapper.Map<IEnumerable<TaskModel>>(await _context.Tasks.Where(t => t.IsDeleted == false).OrderBy(t => t.DueDate).ToListAsync());
        }

        public void AddTask(TaskModel task)
        {
            DateTime currentDateTime = DateTime.UtcNow;

            var newTask = new TaskEntity
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Category = task.Category,
                Created = currentDateTime,
                Updated = currentDateTime
            };

            _context.Tasks.Add(newTask);
        }

        public void UpdateTask(int taskId, TaskModel updatedTask)
        {
            TaskEntity existingTask = GetTaskById(taskId) ?? throw new TaskNotFoundException();

            // Update with new values & fields
            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.Category = updatedTask.Category;
            existingTask.Updated = DateTime.UtcNow;

            _context.Tasks.Update(existingTask);
        }

        public void DeleteTask(int taskId)
        {
            TaskEntity taskToBeDeleted = GetTaskById(taskId) ?? throw new TaskNotFoundException();

            // Soft Delete
            taskToBeDeleted.IsDeleted = true;
            taskToBeDeleted.Updated = DateTime.UtcNow;

            _context.Tasks.Update(taskToBeDeleted);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        private TaskEntity? GetTaskById(int taskId)
        {
            return _context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
        }
    }
}
