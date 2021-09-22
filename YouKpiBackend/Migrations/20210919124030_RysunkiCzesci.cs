using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class RysunkiCzesci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CzesciRysunki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Base64FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Rozszerzenie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CzesciRysunki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CzesciRysunkiInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDodania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DodanoPrzezId = table.Column<int>(type: "int", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CzescId = table.Column<string>(type: "varchar(20)", nullable: true),
                    CzesciRysunkiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CzesciRysunkiInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CzesciRysunkiInfo_Czesci_CzescId",
                        column: x => x.CzescId,
                        principalTable: "Czesci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CzesciRysunkiInfo_CzesciRysunki_CzesciRysunkiId",
                        column: x => x.CzesciRysunkiId,
                        principalTable: "CzesciRysunki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CzesciRysunkiInfo_CzescId",
                table: "CzesciRysunkiInfo",
                column: "CzescId");

            migrationBuilder.CreateIndex(
                name: "IX_CzesciRysunkiInfo_CzesciRysunkiId",
                table: "CzesciRysunkiInfo",
                column: "CzesciRysunkiId",
                unique: true,
                filter: "[CzesciRysunkiId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CzesciRysunkiInfo");

            migrationBuilder.DropTable(
                name: "CzesciRysunki");
        }
    }
}
