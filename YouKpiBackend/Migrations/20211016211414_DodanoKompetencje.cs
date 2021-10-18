using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace YouKpiBackend.Migrations
{
    public partial class DodanoKompetencje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetencesLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencesLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessCompetences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Wartosc = table.Column<int>(type: "int", unicode: false, nullable: false),
                    Cel = table.Column<int>(type: "int", unicode: false, nullable: false),
                    CompetenceLevelId = table.Column<int>(type: "int", nullable: true),
                    OkresOceny = table.Column<int>(type: "int", unicode: false, nullable: false),
                    Typ = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ProcessId = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessCompetences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROCESS_COMPETENCELEVEL_ID",
                        column: x => x.CompetenceLevelId,
                        principalTable: "CompetencesLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PROCESS_PROCESSCOMPETENCE_ID",
                        column: x => x.ProcessId,
                        principalTable: "Process",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessCompetences_CompetenceLevelId",
                table: "ProcessCompetences",
                column: "CompetenceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessCompetences_ProcessId",
                table: "ProcessCompetences",
                column: "ProcessId");

            var script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Seed\InsertCompetencesLevels.sql");
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessCompetences");

            migrationBuilder.DropTable(
                name: "CompetencesLevels");
        }
    }
}
