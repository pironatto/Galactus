using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Migrations
{
    public partial class Dois : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pesGde",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pesMin",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pesNav",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pesRad",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pesGde",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "pesMin",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "pesNav",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "pesRad",
                table: "Planeta");
        }
    }
}
