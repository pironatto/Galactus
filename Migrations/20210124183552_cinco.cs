using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Migrations
{
    public partial class cinco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "consEsta",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consEstaAv",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consEstaOrb",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consFabDro",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consRefAvan",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consRefCar",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consRefNio",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "consEsta",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "consEstaAv",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "consEstaOrb",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "consFabDro",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "consRefAvan",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "consRefCar",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "consRefNio",
                table: "Planeta");
        }
    }
}
