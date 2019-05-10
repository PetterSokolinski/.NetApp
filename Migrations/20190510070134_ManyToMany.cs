using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_ProjectID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ProjectID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Task",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Project",
                newName: "ProjectId");

            migrationBuilder.CreateTable(
                name: "UserProject",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProject", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProject_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserId",
                table: "UserProject",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProject");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Task",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Project",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ProjectID",
                table: "User",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_ProjectID",
                table: "User",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
