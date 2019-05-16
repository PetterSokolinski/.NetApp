using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class manytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_UserID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserID",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Project");

            migrationBuilder.CreateTable(
                name: "ProjectAndUser",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAndUser", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectAndUser_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectAndUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAndUser_UserId",
                table: "ProjectAndUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAndUser");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserID",
                table: "Project",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_UserID",
                table: "Project",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
