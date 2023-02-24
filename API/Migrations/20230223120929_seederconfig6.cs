using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seederconfig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Aquarium_AquariumId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_AquariumId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "AquariumId",
                table: "Measurements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AquariumId",
                table: "Measurements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_AquariumId",
                table: "Measurements",
                column: "AquariumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Aquarium_AquariumId",
                table: "Measurements",
                column: "AquariumId",
                principalTable: "Aquarium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
