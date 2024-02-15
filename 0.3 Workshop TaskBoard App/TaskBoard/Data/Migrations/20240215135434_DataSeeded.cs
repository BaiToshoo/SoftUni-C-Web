using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Board identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Board name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Task identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Task title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Task description"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of creation"),
                    BoardId = table.Column<int>(type: "int", nullable: true, comment: "Board identifier"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Aplication user identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Board Tasks");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6ddb2d2e-836f-4029-9b1e-0c4f2faad3f7", 0, "a65e18e8-4d27-42f2-8d45-55ce98ebf047", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAIAAYagAAAAEEPrdy7XHyTaDWKyrau3B2tzN0BuI9ZVo51N49ixiRnahDlAxBT0aGwwjHfEk1b6Vw==", null, false, "a78d3f67-436b-4ee6-b533-8fbba0587b31", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 30, 15, 54, 33, 299, DateTimeKind.Local).AddTicks(6119), "Implement better styling for all public pages", "6ddb2d2e-836f-4029-9b1e-0c4f2faad3f7", "Improve CSS styles" },
                    { 2, 1, new DateTime(2024, 2, 10, 15, 54, 33, 299, DateTimeKind.Local).AddTicks(6178), "Create Android client app for the TaskBoard RESTful API", "6ddb2d2e-836f-4029-9b1e-0c4f2faad3f7", "Android Client App" },
                    { 3, 2, new DateTime(2024, 2, 14, 15, 54, 33, 299, DateTimeKind.Local).AddTicks(6208), "Create Windows Forms dekstop app client for the TaskBoard RESTful API", "6ddb2d2e-836f-4029-9b1e-0c4f2faad3f7", "Dekstop Client App" },
                    { 4, 3, new DateTime(2024, 2, 14, 15, 54, 33, 299, DateTimeKind.Local).AddTicks(6212), "Implement [Create Task] page for adding new tasks", "6ddb2d2e-836f-4029-9b1e-0c4f2faad3f7", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ddb2d2e-836f-4029-9b1e-0c4f2faad3f7");
        }
    }
}
