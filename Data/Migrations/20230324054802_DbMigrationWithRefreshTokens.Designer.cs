﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Todo_Assignment.API.Data.DbContexts;

#nullable disable

namespace Todo_Assignment.API.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20230324054802_DbMigrationWithRefreshTokens")]
    partial class DbMigrationWithRefreshTokens
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
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
                            Created = new DateTime(2023, 3, 21, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4181),
                            Description = "Completing the first task is a big leap of success.",
                            DueDate = new DateTime(2023, 7, 2, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4175),
                            IsDeleted = false,
                            Title = "First Task",
                            Updated = new DateTime(2023, 3, 24, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4182)
                        },
                        new
                        {
                            Id = 2,
                            Category = "Assignment",
                            Created = new DateTime(2023, 3, 24, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4221),
                            Description = "Create a new To-Do using Angular and ASP.NET for API.",
                            DueDate = new DateTime(2023, 4, 1, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4220),
                            IsDeleted = false,
                            Title = "To-Do List Project",
                            Updated = new DateTime(2023, 3, 24, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4221)
                        },
                        new
                        {
                            Id = 3,
                            Category = "OnBoarding",
                            Created = new DateTime(2023, 2, 22, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4232),
                            Description = "OnBoarding Sessions for AIP interns with HR and Mentors",
                            DueDate = new DateTime(2023, 3, 10, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4232),
                            IsDeleted = false,
                            Title = "OnBoarding Session #1",
                            Updated = new DateTime(2023, 3, 3, 5, 48, 2, 151, DateTimeKind.Utc).AddTicks(4233)
                        });
                });

            modelBuilder.Entity("Todo_Assignment.API.Data.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}