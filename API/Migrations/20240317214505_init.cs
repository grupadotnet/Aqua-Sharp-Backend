using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "AuthId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEO7X30v9YcG0ELCt0w9SY8g+1weZE6bir+SUjlux34k0eNJ55xDCzbEiKAyvM6rniw==");

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "AuthId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEG2IulPNa9UbQC0NvKOpiaBd4ay8q9WXfsZZrDvSiM7Y3Rx0riVc7UHjH149n7YmPQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "AuthId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEHhMmcrg/puWjOu5Of4gsKzRyUgXYrHotOFG4jOQ5gbRWXHvtAolzFANDxbdGHcGBg==");

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "AuthId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEDxOsHt+ZcOH7vmP4rweq8qBNBNBOxG5wAOG2A5VWjbkooEJdy6LwlGlOrGwSwTqVA==");
        }
    }
}
