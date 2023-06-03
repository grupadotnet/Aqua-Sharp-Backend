﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aqua_Sharp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _03061 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aquarium",
                columns: table => new
                {
                    AquariumId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Width = table.Column<long>(type: "bigint", nullable: false),
                    Height = table.Column<long>(type: "bigint", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    PH = table.Column<float>(type: "real", nullable: false),
                    Dawn = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Sunset = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquarium", x => x.AquariumId);
                });

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    ConfigId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    FirstRun = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.ConfigId);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Ph = table.Column<float>(type: "real", nullable: false),
                    TDS = table.Column<long>(type: "bigint", nullable: false),
                    LightOn = table.Column<bool>(type: "boolean", nullable: false),
                    AquariumId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementId);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MeasurementFrequency = table.Column<long>(type: "bigint", nullable: false),
                    ManualMode = table.Column<bool>(type: "boolean", nullable: false),
                    AzureDeviceId = table.Column<string>(type: "text", nullable: false),
                    AquariumId = table.Column<int>(type: "integer", nullable: false)
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
                table: "Config",
                columns: new[] { "ConfigId", "Answer", "FirstRun", "Password", "Question" },
                values: new object[] { 1, "", true, "AQAAAAIAAYagAAAAEL4Pun26YTba5pDt4Fc+EwYhVYl9wcF+0+5g7sNCk7O2f3gy1+4ByFs6HCs/sZXatQ==", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AquariumId",
                table: "Devices",
                column: "AquariumId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Aquarium");
        }
    }
}