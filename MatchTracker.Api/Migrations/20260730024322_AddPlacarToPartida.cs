using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlacarToPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlacarAdversario",
                table: "Partidas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlacarTime",
                table: "Partidas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacarAdversario",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "PlacarTime",
                table: "Partidas");
        }
    }
}
