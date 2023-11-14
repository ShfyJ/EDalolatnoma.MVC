using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDalolatnoma.MVC.Migrations
{
    public partial class Init1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseDatetime",
                table: "Dalolatnomlar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseDatetime",
                table: "Dalolatnomlar",
                type: "text",
                nullable: true);
        }
    }
}
