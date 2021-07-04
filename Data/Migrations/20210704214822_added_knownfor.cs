using Microsoft.EntityFrameworkCore.Migrations;

namespace SinemaskopApp.Data.Migrations
{
    public partial class added_knownfor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KnownFor",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KnownFor",
                table: "Person");
        }
    }
}
