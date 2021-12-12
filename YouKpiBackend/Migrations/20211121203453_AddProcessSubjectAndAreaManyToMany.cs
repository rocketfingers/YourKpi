using Microsoft.EntityFrameworkCore.Migrations;

namespace YouKpiBackend.Migrations
{
    public partial class AddProcessSubjectAndAreaManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Process_ProcessArea_ProcessAreaId",
                table: "Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Process_ProcessSubject_ProcessSubjectId",
                table: "Process");

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

            migrationBuilder.CreateTable(
                name: "ProcessesProcessAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ProcessAreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesProcessAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessesProcessAreas",
                        column: x => x.ProcessId,
                        principalTable: "Process",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessesProcessAreas_ProcessArea_ProcessAreaId",
                        column: x => x.ProcessAreaId,
                        principalTable: "ProcessArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessesProcessSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ProcessSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesProcessSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessesProcessSubjects",
                        column: x => x.ProcessId,
                        principalTable: "Process",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessesProcessSubjects_ProcessSubject_ProcessSubjectId",
                        column: x => x.ProcessSubjectId,
                        principalTable: "ProcessSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesProcessAreas_ProcessAreaId",
                table: "ProcessesProcessAreas",
                column: "ProcessAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesProcessAreas_ProcessId",
                table: "ProcessesProcessAreas",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesProcessSubjects_ProcessId",
                table: "ProcessesProcessSubjects",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesProcessSubjects_ProcessSubjectId",
                table: "ProcessesProcessSubjects",
                column: "ProcessSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessesProcessAreas");

            migrationBuilder.DropTable(
                name: "ProcessesProcessSubjects");

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
        }
    }
}
