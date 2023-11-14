using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDalolatnoma.MVC.Migrations
{
    public partial class Init12345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDatetime",
                table: "Dalolatnomlar",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseDatetime",
                table: "Dalolatnomlar");
        }
    }
}
