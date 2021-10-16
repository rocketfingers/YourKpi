using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace YouKpiBackend.Migrations
{
    public partial class AddMediumsDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediumsDictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumsDictionary", x => x.Id);
                });
            var script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Seed\InsertMediumsDictionary.sql");
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediumsDictionary");
        }
    }
}
