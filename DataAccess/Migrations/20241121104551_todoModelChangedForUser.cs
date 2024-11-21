using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class todoModelChangedForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "Todos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CreatedUserId",
                table: "Todos",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_CreatedUserId",
                table: "Todos",
                column: "CreatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_CreatedUserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_CreatedUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Todos");
        }
    }
}
