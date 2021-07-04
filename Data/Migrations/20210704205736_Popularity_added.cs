using Microsoft.EntityFrameworkCore.Migrations;

namespace SinemaskopApp.Data.Migrations
{
    public partial class Popularity_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Popularity",
                table: "Person",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Person");
        }
    }
}
