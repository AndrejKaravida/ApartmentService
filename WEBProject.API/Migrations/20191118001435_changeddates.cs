using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBProject.API.Migrations
{
    public partial class changeddates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "ReservedDate");

            migrationBuilder.AddColumn<int>(
                name: "DayFromToday",
                table: "ReservedDate",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayFromToday",
                table: "ReservedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ReservedDate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
