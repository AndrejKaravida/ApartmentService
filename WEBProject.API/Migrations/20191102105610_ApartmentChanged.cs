using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBProject.API.Migrations
{
    public partial class ApartmentChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Users_UserId",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Apartments",
                newName: "HostId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_UserId",
                table: "Apartments",
                newName: "IX_Apartments_HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Users_HostId",
                table: "Apartments",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Users_HostId",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "HostId",
                table: "Apartments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_HostId",
                table: "Apartments",
                newName: "IX_Apartments_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Users_UserId",
                table: "Apartments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
