using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seederconfig16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstRun",
                table: "Config");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Config",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Config",
                table: "Config",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Config",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Config");

            migrationBuilder.AddColumn<bool>(
                name: "FirstRun",
                table: "Config",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }
    }
}
