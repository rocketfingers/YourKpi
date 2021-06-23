using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace YouKpiBackend.Migrations
{
    public partial class CreateViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string script = File.ReadAllText(Directory.GetCurrentDirectory()+@"\SQL\Views\VActivityHistory.sql");
            migrationBuilder.Sql(script);
            script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Views\VProdExe.sql");
            migrationBuilder.Sql(script);
            script = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SQL\Views\VOtifReport.sql");
            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // mozna dopisac drop....
        }
    }
}
