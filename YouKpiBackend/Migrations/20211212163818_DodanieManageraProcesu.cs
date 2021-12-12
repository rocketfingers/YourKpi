using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class DodanieManageraProcesu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessManagerId",
                table: "Process",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Process_ProcessManagerId",
                table: "Process",
                column: "ProcessManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Process_ManagerOfProcess",
                table: "Process",
                column: "ProcessManagerId",
                principalTable: "Pracownik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Process_ManagerOfProcess",
                table: "Process");

            migrationBuilder.DropIndex(
                name: "IX_Process_ProcessManagerId",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "ProcessManagerId",
                table: "Process");
        }
    }
}
