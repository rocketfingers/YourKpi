using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class RozbudowaRysunkow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProduktyRysunki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Base64FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Rozszerzenie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktyRysunki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProduktyRysunkiInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDodania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DodanoPrzezId = table.Column<int>(type: "int", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProduktId = table.Column<int>(type: "int", nullable: true),
                    ProduktyRysunkiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktyRysunkiInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProduktyRysunkiInfo_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduktyRysunkiInfo_ProduktyRysunki_ProduktyRysunkiId",
                        column: x => x.ProduktyRysunkiId,
                        principalTable: "ProduktyRysunki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktyRysunkiInfo_ProduktId",
                table: "ProduktyRysunkiInfo",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduktyRysunkiInfo_ProduktyRysunkiId",
                table: "ProduktyRysunkiInfo",
                column: "ProduktyRysunkiId",
                unique: true,
                filter: "[ProduktyRysunkiId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktyRysunkiInfo");

            migrationBuilder.DropTable(
                name: "ProduktyRysunki");
        }
    }
}
