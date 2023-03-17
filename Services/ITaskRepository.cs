using Todo_Assignment.API.Data.Entities;
using Todo_Assignment.API.Models;

namespace Todo_Assignment.API.Services
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetTasksAsync();
        void AddTask(TaskModel task);
        void UpdateTask(int taskId, TaskModel task);
        void DeleteTask(int taskId);
        Task<bool> SaveChangesAsync();
    }
}
