﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Todo_Assignment.API.Data.DbContexts;

#nullable disable

namespace Todo_Assignment.API.Migrations
{
    [DbContext(typeof(TaskContext))]
    partial class TaskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Todo_Assignment.API.Data.Entities.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Urgent",
                            Created = new DateTime(2023, 3, 14, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3718),
                            Description = "Completing the first task is a big leap of success.",
                            DueDate = new DateTime(2023, 6, 25, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3711),
                            IsDeleted = false,
                            Title = "First Task",
                            Updated = new DateTime(2023, 3, 17, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3719)
                        },
                        new
                        {
                            Id = 2,
                            Category = "Assignment",
                            Created = new DateTime(2023, 3, 17, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3814),
                            Description = "Create a new To-Do using Angular and ASP.NET for API.",
                            DueDate = new DateTime(2023, 3, 25, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3813),
                            IsDeleted = false,
                            Title = "To-Do List Project",
                            Updated = new DateTime(2023, 3, 17, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3814)
                        },
                        new
                        {
                            Id = 3,
                            Category = "OnBoarding",
                            Created = new DateTime(2023, 2, 15, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3829),
                            Description = "OnBoarding Sessions for AIP interns with HR and Mentors",
                            DueDate = new DateTime(2023, 3, 3, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3828),
                            IsDeleted = false,
                            Title = "OnBoarding Session #1",
                            Updated = new DateTime(2023, 2, 24, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3830)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
