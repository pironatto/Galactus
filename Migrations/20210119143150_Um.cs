using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Migrations
{
    public partial class Um : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AstCarbono",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AstLivre",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AstNiobio",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AstPlutonio",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Carbono",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Niobio",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Plutonio",
                table: "Planeta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AstCarbono",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "AstLivre",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "AstNiobio",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "AstPlutonio",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "Carbono",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "Niobio",
                table: "Planeta");

            migrationBuilder.DropColumn(
                name: "Plutonio",
                table: "Planeta");
        }
    }
}
