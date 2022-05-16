using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibrary.Infrastructure.Migrations
{
    public partial class libatr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Users_UserId",
                table: "Libraries");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Libraries",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                newName: "IX_Libraries_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Users_OwnerId",
                table: "Libraries",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Users_OwnerId",
                table: "Libraries");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Libraries",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Libraries_OwnerId",
                table: "Libraries",
                newName: "IX_Libraries_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Users_UserId",
                table: "Libraries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
