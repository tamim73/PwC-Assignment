using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Context.Migrations
{
    public partial class i : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Assignment");

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                schema: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    StoryId = table.Column<int>(type: "int", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_ApplicationUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "Assignment",
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                schema: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_Posts_TopicId",
                        column: x => x.TopicId,
                        principalSchema: "Assignment",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Assignment",
                table: "ApplicationUsers",
                columns: new[] { "Id", "CreationDateTime", "Name", "Password", "Role", "Username" },
                values: new object[] { 1, new DateTime(2021, 1, 22, 0, 28, 38, 797, DateTimeKind.Local).AddTicks(1588), "System Administrator", "AFNMjEQsD+iKDUfUsQDBNX64Z5k9A0jRYKCY/BVmKaVPhBNg", "Admin", "sysadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                schema: "Assignment",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_StoryId",
                schema: "Assignment",
                table: "Posts",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_TopicId",
                schema: "Assignment",
                table: "Stories",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Stories_StoryId",
                schema: "Assignment",
                table: "Posts",
                column: "StoryId",
                principalSchema: "Assignment",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ApplicationUsers_AuthorId",
                schema: "Assignment",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Stories_StoryId",
                schema: "Assignment",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "ApplicationUsers",
                schema: "Assignment");

            migrationBuilder.DropTable(
                name: "Stories",
                schema: "Assignment");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Assignment");
        }
    }
}
