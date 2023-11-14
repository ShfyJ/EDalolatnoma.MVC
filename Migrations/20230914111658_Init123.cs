using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDalolatnoma.MVC.Migrations
{
    public partial class Init123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseDatetime",
                table: "Dalolatnomlar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseMessage",
                table: "Dalolatnomlar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SendStatus",
                table: "Dalolatnomlar",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseDatetime",
                table: "Dalolatnomlar");

            migrationBuilder.DropColumn(
                name: "ResponseMessage",
                table: "Dalolatnomlar");

            migrationBuilder.DropColumn(
                name: "SendStatus",
                table: "Dalolatnomlar");
        }
    }
}
