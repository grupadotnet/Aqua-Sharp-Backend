using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seederconfig20 : Migration
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
                oldDefaultValue: "Password");

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "Id", "Answer", "FirstRun", "Password", "Question" },
                values: new object[] { 1, "", true, "password", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "Password",
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
