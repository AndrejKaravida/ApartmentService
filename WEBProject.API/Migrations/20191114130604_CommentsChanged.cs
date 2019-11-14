using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBProject.API.Migrations
{
    public partial class CommentsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Comments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Comments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Comments");
        }
    }
}
