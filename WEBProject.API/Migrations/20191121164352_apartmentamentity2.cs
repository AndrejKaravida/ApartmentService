using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBProject.API.Migrations
{
    public partial class apartmentamentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmentity_Amentities_AmentityId",
                table: "ApartmentAmentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmentity_Apartments_ApartmentId",
                table: "ApartmentAmentity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartmentAmentity",
                table: "ApartmentAmentity");

            migrationBuilder.RenameTable(
                name: "ApartmentAmentity",
                newName: "ApartmentAmentities");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentAmentity_AmentityId",
                table: "ApartmentAmentities",
                newName: "IX_ApartmentAmentities_AmentityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartmentAmentities",
                table: "ApartmentAmentities",
                columns: new[] { "ApartmentId", "AmentityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmentities_Amentities_AmentityId",
                table: "ApartmentAmentities",
                column: "AmentityId",
                principalTable: "Amentities",
                principalColumn: "AmentityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmentities_Apartments_ApartmentId",
                table: "ApartmentAmentities",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "ApartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmentities_Amentities_AmentityId",
                table: "ApartmentAmentities");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmentities_Apartments_ApartmentId",
                table: "ApartmentAmentities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartmentAmentities",
                table: "ApartmentAmentities");

            migrationBuilder.RenameTable(
                name: "ApartmentAmentities",
                newName: "ApartmentAmentity");

            migrationBuilder.RenameIndex(
                name: "IX_ApartmentAmentities_AmentityId",
                table: "ApartmentAmentity",
                newName: "IX_ApartmentAmentity_AmentityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartmentAmentity",
                table: "ApartmentAmentity",
                columns: new[] { "ApartmentId", "AmentityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmentity_Amentities_AmentityId",
                table: "ApartmentAmentity",
                column: "AmentityId",
                principalTable: "Amentities",
                principalColumn: "AmentityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmentity_Apartments_ApartmentId",
                table: "ApartmentAmentity",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "ApartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
