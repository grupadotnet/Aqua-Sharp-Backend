using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seederconfig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Aquarium_AquariumId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_AquariumId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "AquariumId",
                table: "Devices");

            migrationBuilder.AddColumn<int>(
                name: "AquariumId",
                table: "Measurements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Aquarium",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Aquarium_DeviceId",
                table: "Aquarium",
                column: "DeviceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aquarium_Devices_DeviceId",
                table: "Aquarium",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aquarium_Devices_DeviceId",
                table: "Aquarium");

            migrationBuilder.DropIndex(
                name: "IX_Aquarium_DeviceId",
                table: "Aquarium");

            migrationBuilder.DropColumn(
                name: "AquariumId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Aquarium");

            migrationBuilder.AddColumn<int>(
                name: "AquariumId",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AquariumId",
                table: "Devices",
                column: "AquariumId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Aquarium_AquariumId",
                table: "Devices",
                column: "AquariumId",
                principalTable: "Aquarium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
