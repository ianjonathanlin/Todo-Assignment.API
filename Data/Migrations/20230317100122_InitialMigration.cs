using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Todo_Assignment.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Category = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Category", "Created", "Description", "DueDate", "IsDeleted", "Title", "Updated" },
                values: new object[,]
                {
                    { 1, "Urgent", new DateTime(2023, 3, 14, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3718), "Completing the first task is a big leap of success.", new DateTime(2023, 6, 25, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3711), false, "First Task", new DateTime(2023, 3, 17, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3719) },
                    { 2, "Assignment", new DateTime(2023, 3, 17, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3814), "Create a new To-Do using Angular and ASP.NET for API.", new DateTime(2023, 3, 25, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3813), false, "To-Do List Project", new DateTime(2023, 3, 17, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3814) },
                    { 3, "OnBoarding", new DateTime(2023, 2, 15, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3829), "OnBoarding Sessions for AIP interns with HR and Mentors", new DateTime(2023, 3, 3, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3828), false, "OnBoarding Session #1", new DateTime(2023, 2, 24, 10, 1, 22, 705, DateTimeKind.Utc).AddTicks(3830) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
