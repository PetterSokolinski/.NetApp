using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class Thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Task",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Task_ProjectId",
                table: "Task",
                newName: "IX_Task_ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectID",
                table: "Task",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectID",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Task",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_ProjectID",
                table: "Task",
                newName: "IX_Task_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
