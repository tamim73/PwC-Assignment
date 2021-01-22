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
                name: "Stories",
                schema: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    IsTopic = table.Column<bool>(type: "bit", nullable: false),
                    TopicForStoryId = table.Column<int>(type: "int", nullable: true),
                    PostForStoryId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Posts_Stories_PostForStoryId",
                        column: x => x.PostForStoryId,
                        principalSchema: "Assignment",
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Stories_TopicForStoryId",
                        column: x => x.TopicForStoryId,
                        principalSchema: "Assignment",
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Assignment",
                table: "ApplicationUsers",
                columns: new[] { "Id", "CreationDateTime", "Name", "Password", "Role", "Username" },
                values: new object[] { 1, new DateTime(2021, 1, 22, 18, 41, 10, 998, DateTimeKind.Local).AddTicks(3834), "System Administrator", "7X+n5MaAc0iBmmbis0q/TC4Ji019t+04rHyMveqyGqLuxD6s", "Admin", "sysadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                schema: "Assignment",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostForStoryId",
                schema: "Assignment",
                table: "Posts",
                column: "PostForStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TopicForStoryId",
                schema: "Assignment",
                table: "Posts",
                column: "TopicForStoryId",
                unique: true,
                filter: "[TopicForStoryId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Assignment");

            migrationBuilder.DropTable(
                name: "ApplicationUsers",
                schema: "Assignment");

            migrationBuilder.DropTable(
                name: "Stories",
                schema: "Assignment");
        }
    }
}
