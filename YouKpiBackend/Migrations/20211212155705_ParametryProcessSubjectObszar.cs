using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class ParametryProcessSubjectObszar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectAreaGroup",
                table: "ProcessArea",
                newName: "ProcessAreaId");

            migrationBuilder.AddColumn<string>(
                name: "NazwaEn",
                table: "ProcessSubject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessLevel",
                table: "ProcessSubject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectAreaId",
                table: "ProcessSubject",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessLevel",
                table: "ProcessArea",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazwaEn",
                table: "ProcessSubject");

            migrationBuilder.DropColumn(
                name: "ProcessLevel",
                table: "ProcessSubject");

            migrationBuilder.DropColumn(
                name: "SubjectAreaId",
                table: "ProcessSubject");

            migrationBuilder.DropColumn(
                name: "ProcessLevel",
                table: "ProcessArea");

            migrationBuilder.RenameColumn(
                name: "ProcessAreaId",
                table: "ProcessArea",
                newName: "SubjectAreaGroup");
        }
    }
}
