﻿using Microsoft.EntityFrameworkCore;
using Todo_Assignment.API.Data.Entities;

namespace Todo_Assignment.API.Data.DbContexts
{
    public class TodoDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public TodoDbContext(DbContextOptions<TodoDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        public DbSet<UserEntity> Users => Set<UserEntity>();

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
                    IsDeleted = false,
                    Created = DateTime.UtcNow.AddDays(-3),
                    Updated = DateTime.UtcNow
                });

            modelBuilder.Entity<TaskEntity>()
                .HasData(new
                {
                    Id = 2,
                    Title = "To-Do List Project",
                    Description = "Create a new To-Do using Angular and ASP.NET for API.",
                    DueDate = DateTime.UtcNow.AddDays(8),
                    Category = "Assignment",
                    IsDeleted = false,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                });

            modelBuilder.Entity<TaskEntity>()
                .HasData(new
                {
                    Id = 3,
                    Title = "OnBoarding Session #1",
                    Description = "OnBoarding Sessions for AIP interns with HR and Mentors",
                    DueDate = DateTime.UtcNow.AddDays(-14),
                    Category = "OnBoarding",
                    IsDeleted = false,
                    Created = DateTime.UtcNow.AddDays(-30),
                    Updated = DateTime.UtcNow.AddDays(-21)
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
