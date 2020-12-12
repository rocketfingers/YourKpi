using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class RelacjaDoOfferLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferOfferLinesConnection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferOfferLinesConnection",
                columns: table => new
                {
                    OfferID = table.Column<int>(type: "int", nullable: true),
                    OfferLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__OfferOffe__Offer__01D345B0",
                        column: x => x.OfferLineId,
                        principalTable: "OfferLines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OfferOffe__Offer__7E02B4CC",
                        column: x => x.OfferID,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferOfferLinesConnection_OfferID",
                table: "OfferOfferLinesConnection",
                column: "OfferID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOfferLinesConnection_OfferLineId",
                table: "OfferOfferLinesConnection",
                column: "OfferLineId");
        }
    }
}
