using Microsoft.EntityFrameworkCore.Migrations;

namespace SinemaskopApp.Data.Migrations
{
    public partial class after_create_people_code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genre",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GenMov",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreKey = table.Column<int>(type: "int", nullable: false),
                    MovieKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenMov", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerMovAct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonKey = table.Column<int>(type: "int", nullable: false),
                    MovieKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerMovAct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerMovDir",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonKey = table.Column<int>(type: "int", nullable: false),
                    MovieKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerMovDir", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseMovLat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseMovLat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseMovLik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseMovLik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseMovWat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseMovWat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsePerLik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsePerLik", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenMov");

            migrationBuilder.DropTable(
                name: "PerMovAct");

            migrationBuilder.DropTable(
                name: "PerMovDir");

            migrationBuilder.DropTable(
                name: "UseMovLat");

            migrationBuilder.DropTable(
                name: "UseMovLik");

            migrationBuilder.DropTable(
                name: "UseMovWat");

            migrationBuilder.DropTable(
                name: "UsePerLik");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genre",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
