using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seederconfig18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Config",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "password");

            migrationBuilder.AddColumn<bool>(
                name: "FirstRun",
                table: "Config",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstRun",
                table: "Config");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "password",
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
