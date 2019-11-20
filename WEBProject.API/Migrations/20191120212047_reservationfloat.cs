using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBProject.API.Migrations
{
    public partial class reservationfloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedDate",
                table: "BlockedDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amentities",
                table: "Amentities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BlockedDate");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Amentities");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Amentities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedDate",
                table: "BlockedDate",
                column: "Date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amentities",
                table: "Amentities",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedDate",
                table: "BlockedDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amentities",
                table: "Amentities");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BlockedDate",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Amentities",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Amentities",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedDate",
                table: "BlockedDate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amentities",
                table: "Amentities",
                column: "Id");
        }
    }
}
