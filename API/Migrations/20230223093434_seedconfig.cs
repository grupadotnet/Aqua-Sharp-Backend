using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seedconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aquarium_Devices_DeviceId",
                table: "Aquarium");

            migrationBuilder.DropIndex(
                name: "IX_Aquarium_DeviceId",
                table: "Aquarium");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Aquarium");

            migrationBuilder.DropColumn(
                name: "IdDevice",
                table: "Aquarium");

            migrationBuilder.RenameColumn(
                name: "IdAquarium",
                table: "Measurements",
                newName: "AquariumId");

            migrationBuilder.AddColumn<int>(
                name: "AquariumId",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "password",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "FirstRun",
                table: "Config",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_AquariumId",
                table: "Measurements",
                column: "AquariumId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Aquarium_AquariumId",
                table: "Measurements",
                column: "AquariumId",
                principalTable: "Aquarium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Aquarium_AquariumId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Aquarium_AquariumId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_AquariumId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Devices_AquariumId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "AquariumId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "AquariumId",
                table: "Measurements",
                newName: "IdAquarium");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Config",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "password");

            migrationBuilder.AlterColumn<bool>(
                name: "FirstRun",
                table: "Config",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Aquarium",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDevice",
                table: "Aquarium",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Aquarium_DeviceId",
                table: "Aquarium",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aquarium_Devices_DeviceId",
                table: "Aquarium",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
