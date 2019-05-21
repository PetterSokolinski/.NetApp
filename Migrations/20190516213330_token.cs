using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "User",
                nullable: false,
                defaultValue: false);
        }
    }
}
