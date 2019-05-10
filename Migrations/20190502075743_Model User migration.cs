using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class ModelUsermigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_ProjectId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "User",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_User_ProjectId",
                table: "User",
                newName: "IX_User_ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_ProjectID",
                table: "User",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_ProjectID",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "User",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_User_ProjectID",
                table: "User",
                newName: "IX_User_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_ProjectId",
                table: "User",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
