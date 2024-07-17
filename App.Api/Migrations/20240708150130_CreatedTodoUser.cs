using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTodoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Todo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todo_UserId1",
                table: "Todo",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_User_UserId1",
                table: "Todo",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_User_UserId1",
                table: "Todo");

            migrationBuilder.DropIndex(
                name: "IX_Todo_UserId1",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Todo");
        }
    }
}
