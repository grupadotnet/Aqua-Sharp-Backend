using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seederconfig21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Config",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "FIEJEFJ",
                oldClrType: typeof(string),
                oldType: "text");

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

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "FIEJEFJ",
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Config",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "FIEJEFJ");

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

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Config",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "FIEJEFJ");

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "Id", "Answer", "FirstRun", "Password", "Question" },
                values: new object[] { 1, "", true, "password", "" });
        }
    }
}
