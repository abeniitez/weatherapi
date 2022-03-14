using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adbeniz.Weather.Restful.Domain.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "WeatherHistorical",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 11, 8, 38, 56, 230, DateTimeKind.Utc).AddTicks(2630),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 11, 3, 58, 43, 688, DateTimeKind.Utc).AddTicks(6401));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "WeatherHistorical",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 11, 3, 58, 43, 688, DateTimeKind.Utc).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 11, 8, 38, 56, 230, DateTimeKind.Utc).AddTicks(2630));
        }
    }
}
