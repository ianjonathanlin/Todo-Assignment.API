using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Todo_Assignment.API.Migrations
{
    /// <inheritdoc />
    public partial class DbMigrationWithUserTable : Migration
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Category", "Created", "Description", "DueDate", "IsDeleted", "Title", "Updated" },
                values: new object[,]
                {
                    { 1, "Urgent", new DateTime(2023, 3, 18, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2501), "Completing the first task is a big leap of success.", new DateTime(2023, 6, 29, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2494), false, "First Task", new DateTime(2023, 3, 21, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2502) },
                    { 2, "Assignment", new DateTime(2023, 3, 21, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2541), "Create a new To-Do using Angular and ASP.NET for API.", new DateTime(2023, 3, 29, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2540), false, "To-Do List Project", new DateTime(2023, 3, 21, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2541) },
                    { 3, "OnBoarding", new DateTime(2023, 2, 19, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2556), "OnBoarding Sessions for AIP interns with HR and Mentors", new DateTime(2023, 3, 7, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2555), false, "OnBoarding Session #1", new DateTime(2023, 2, 28, 6, 49, 54, 876, DateTimeKind.Utc).AddTicks(2557) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
