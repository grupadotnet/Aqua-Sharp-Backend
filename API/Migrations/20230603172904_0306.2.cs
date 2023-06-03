using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _03062 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AzureDeviceId",
                table: "Devices",
                newName: "AzureDeviceName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AzureDeviceName",
                table: "Devices",
                newName: "AzureDeviceId");
        }
    }
}
