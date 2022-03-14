using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adbeniz.Weather.Restful.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WeatherHistorical",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weather = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    ThermalSensation = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 3, 10, 4, 30, 34, 923, DateTimeKind.Utc).AddTicks(6342))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherHistorical", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeatherHistorical_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherHistorical_CityId",
                table: "WeatherHistorical",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherHistorical");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
