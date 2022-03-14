using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adbeniz.Weather.Restful.Domain.Migrations
{
    public partial class AddUserENtity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "WeatherHistorical",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 11, 3, 58, 43, 688, DateTimeKind.Utc).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 10, 4, 30, 34, 923, DateTimeKind.Utc).AddTicks(6342));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "WeatherHistorical",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 10, 4, 30, 34, 923, DateTimeKind.Utc).AddTicks(6342),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 11, 3, 58, 43, 688, DateTimeKind.Utc).AddTicks(6401));
        }
    }
}
