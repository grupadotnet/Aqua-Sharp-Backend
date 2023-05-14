using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class PhInMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Ph",
                table: "Measurements",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ph",
                table: "Measurements");
        }
    }
}
