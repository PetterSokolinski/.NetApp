using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class ModelProjectmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Project",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Project");
        }
    }
}
