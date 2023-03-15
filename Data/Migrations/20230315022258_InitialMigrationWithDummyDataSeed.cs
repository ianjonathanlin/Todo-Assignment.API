using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Todo_Assignment.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationWithDummyDataSeed : Migration
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
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LatestUpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Category", "CreatedDateTime", "Description", "DueDate", "IsDeleted", "LatestUpdatedDateTime", "Title" },
                values: new object[,]
                {
                    { 1, "Urgent", new DateTime(2023, 3, 12, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9184), "Completing the first task is a big leap of success.", new DateTime(2023, 6, 23, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9176), false, new DateTime(2023, 3, 15, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9186), "First Task" },
                    { 2, "Assignment", new DateTime(2023, 3, 15, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9238), "Create a new To-Do using Angular and ASP.NET for API.", new DateTime(2023, 3, 23, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9237), false, new DateTime(2023, 3, 15, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9238), "To-Do List Project" },
                    { 3, "OnBoarding", new DateTime(2023, 4, 14, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9254), "OnBoarding Sessions for AIP interns with HR and Mentors", new DateTime(2023, 3, 29, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9253), true, new DateTime(2023, 4, 5, 2, 22, 58, 218, DateTimeKind.Utc).AddTicks(9254), "OnBoarding Session #1" }
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
