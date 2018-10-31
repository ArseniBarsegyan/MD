using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MD.Migrations.Migrations.Identity
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageContent",
                schema: "dbo",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContent",
                schema: "dbo",
                table: "AspNetUsers");
        }
    }
}
