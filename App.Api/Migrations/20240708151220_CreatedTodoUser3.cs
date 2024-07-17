using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTodoUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_User_UserId1",
                table: "Todo");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Todo",
                newName: "CategoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_Todo_UserId1",
                table: "Todo",
                newName: "IX_Todo_CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_Category_CategoryId1",
                table: "Todo",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_Category_CategoryId1",
                table: "Todo");

            migrationBuilder.RenameColumn(
                name: "CategoryId1",
                table: "Todo",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Todo_CategoryId1",
                table: "Todo",
                newName: "IX_Todo_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_User_UserId1",
                table: "Todo",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
