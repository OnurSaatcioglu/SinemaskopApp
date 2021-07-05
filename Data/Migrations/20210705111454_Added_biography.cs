using Microsoft.EntityFrameworkCore.Migrations;

namespace SinemaskopApp.Data.Migrations
{
    public partial class Added_biography : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Person");
        }
    }
}
