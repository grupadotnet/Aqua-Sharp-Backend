using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class migII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auth",
                columns: table => new
                {
                    AuthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstRun = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth", x => x.AuthId);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Ph = table.Column<float>(type: "real", nullable: false),
                    TDS = table.Column<long>(type: "bigint", nullable: false),
                    LightOn = table.Column<bool>(type: "bit", nullable: false),
                    AquariumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Auth_AuthId",
                        column: x => x.AuthId,
                        principalTable: "Auth",
                        principalColumn: "AuthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aquarium",
                columns: table => new
                {
                    AquariumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Width = table.Column<long>(type: "bigint", nullable: false),
                    Height = table.Column<long>(type: "bigint", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    PH = table.Column<float>(type: "real", nullable: false),
                    Dawn = table.Column<TimeSpan>(type: "time", nullable: false),
                    Sunset = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquarium", x => x.AquariumId);
                    table.ForeignKey(
                        name: "FK_Aquarium_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementFrequency = table.Column<long>(type: "bigint", nullable: false),
                    ManualMode = table.Column<bool>(type: "bit", nullable: false),
                    AzureDeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AquariumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Aquarium_AquariumId",
                        column: x => x.AquariumId,
                        principalTable: "Aquarium",
                        principalColumn: "AquariumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Auth",
                columns: new[] { "AuthId", "Answer", "FirstRun", "Password", "Question" },
                values: new object[] { 1, "", true, "AQAAAAIAAYagAAAAEL4Pun26YTba5pDt4Fc+EwYhVYl9wcF+0+5g7sNCk7O2f3gy1+4ByFs6HCs/sZXatQ==", "" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "all" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthId", "FirstName", "LastName", "Login", "RoleId" },
                values: new object[] { 1, 1, "Admin", "Admin", "Admin", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Aquarium_UserId",
                table: "Aquarium",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AquariumId",
                table: "Devices",
                column: "AquariumId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthId",
                table: "Users",
                column: "AuthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Aquarium");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Auth");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
