using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDalolatnoma.MVC.Migrations
{
    public partial class Init12347 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendStatus",
                table: "Dalolatnomlar");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Dalolatnomlar",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Dalolatnomlar");

            migrationBuilder.AddColumn<string>(
                name: "SendStatus",
                table: "Dalolatnomlar",
                type: "text",
                nullable: true);
        }
    }
}
