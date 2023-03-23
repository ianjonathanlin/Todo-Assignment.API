using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Todo_Assignment.API.Migrations
{
    /// <inheritdoc />
    public partial class DbMigrationWithRefreshTokens : Migration
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Category", "Created", "Description", "DueDate", "IsDeleted", "Title", "Updated" },
                values: new object[,]
                {
                    { 1, "Urgent", new DateTime(2023, 3, 20, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9429), "Completing the first task is a big leap of success.", new DateTime(2023, 7, 1, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9420), false, "First Task", new DateTime(2023, 3, 23, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9429) },
                    { 2, "Assignment", new DateTime(2023, 3, 23, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9461), "Create a new To-Do using Angular and ASP.NET for API.", new DateTime(2023, 3, 31, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9460), false, "To-Do List Project", new DateTime(2023, 3, 23, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9461) },
                    { 3, "OnBoarding", new DateTime(2023, 2, 21, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9469), "OnBoarding Sessions for AIP interns with HR and Mentors", new DateTime(2023, 3, 9, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9469), false, "OnBoarding Session #1", new DateTime(2023, 3, 2, 7, 53, 6, 839, DateTimeKind.Utc).AddTicks(9469) }
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
