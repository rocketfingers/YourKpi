using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace YouKpiBackend.Migrations
{
    public partial class AddProcessSubjectAndArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessAreaId",
                table: "Process",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessSubjectId",
                table: "Process",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProcessArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NazwaEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    SubjectAreaGroup = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessSubject", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Process_ProcessAreaId",
                table: "Process",
                column: "ProcessAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_ProcessSubjectId",
                table: "Process",
                column: "ProcessSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Process_ProcessArea_ProcessAreaId",
                table: "Process",
                column: "ProcessAreaId",
                principalTable: "ProcessArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_ProcessSubject_ProcessSubjectId",
                table: "Process",
                column: "ProcessSubjectId",
                principalTable: "ProcessSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            var script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Seed\InsertProcessSubjects.sql");
            migrationBuilder.Sql(script);
            script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Seed\InsertProcessAreas.sql");
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Process_ProcessArea_ProcessAreaId",
                table: "Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Process_ProcessSubject_ProcessSubjectId",
                table: "Process");

            migrationBuilder.DropTable(
                name: "ProcessArea");

            migrationBuilder.DropTable(
                name: "ProcessSubject");

            migrationBuilder.DropIndex(
                name: "IX_Process_ProcessAreaId",
                table: "Process");

            migrationBuilder.DropIndex(
                name: "IX_Process_ProcessSubjectId",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "ProcessAreaId",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "ProcessSubjectId",
                table: "Process");
        }
    }
}
