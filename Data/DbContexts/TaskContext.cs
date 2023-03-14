using Microsoft.EntityFrameworkCore;
using Todo_Assignment.API.Data.Entities;

namespace Todo_Assignment.API.Data.DbContexts
{
    public class TaskContext : DbContext
    {
        private readonly IConfiguration _config;

        public TaskContext(DbContextOptions<TaskContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("NpgDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .HasData(new
                {
                    Id = 1,
                    Title = "First Task",
                    Description = "Completing the first task is a big leap of success.",
                    DueDate = DateTime.UtcNow.AddDays(100),
                    Category = "Urgent",
                    IsDeleted = true,
                    CreatedDateTime = DateTime.UtcNow.AddDays(-3),
                    LatestUpdatedDateTime = DateTime.UtcNow
                });

            modelBuilder.Entity<TaskEntity>()
                .HasData(new
                {
                    Id = 2,
                    Title = "To-Do List Project",
                    Description = "Create a new To-Do that suits for any use cases with Angular and ASP.NET.",
                    DueDate = DateTime.UtcNow.AddDays(8),
                    Category = "Assignment",
                    IsDeleted = false,
                    CreatedDateTime = DateTime.UtcNow,
                    LatestUpdatedDateTime = DateTime.UtcNow
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
