using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBProject.API.Migrations
{
    public partial class apartmentamentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amentities_Apartments_ApartmentId",
                table: "Amentities");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Apartments_AppartmentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Amentities_ApartmentId",
                table: "Amentities");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Amentities");

            migrationBuilder.RenameColumn(
                name: "AppartmentId",
                table: "Reservations",
                newName: "AppartmentApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_AppartmentId",
                table: "Reservations",
                newName: "IX_Reservations_AppartmentApartmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Apartments",
                newName: "ApartmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Amentities",
                newName: "AmentityId");

            migrationBuilder.CreateTable(
                name: "ApartmentAmentity",
                columns: table => new
                {
                    ApartmentId = table.Column<int>(nullable: false),
                    AmentityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentAmentity", x => new { x.ApartmentId, x.AmentityId });
                    table.ForeignKey(
                        name: "FK_ApartmentAmentity_Amentities_AmentityId",
                        column: x => x.AmentityId,
                        principalTable: "Amentities",
                        principalColumn: "AmentityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentAmentity_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAmentity_AmentityId",
                table: "ApartmentAmentity",
                column: "AmentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Apartments_AppartmentApartmentId",
                table: "Reservations",
                column: "AppartmentApartmentId",
                principalTable: "Apartments",
                principalColumn: "ApartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Apartments_AppartmentApartmentId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ApartmentAmentity");

            migrationBuilder.RenameColumn(
                name: "AppartmentApartmentId",
                table: "Reservations",
                newName: "AppartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_AppartmentApartmentId",
                table: "Reservations",
                newName: "IX_Reservations_AppartmentId");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                table: "Apartments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AmentityId",
                table: "Amentities",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Amentities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amentities_ApartmentId",
                table: "Amentities",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amentities_Apartments_ApartmentId",
                table: "Amentities",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Apartments_AppartmentId",
                table: "Reservations",
                column: "AppartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
