using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibrary.Infrastructure.Migrations
{
    public partial class bookCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "Books",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Books");
        }
    }
}
