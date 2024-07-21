using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_food_AspNetUsers_ExtendIdentityUserId",
                table: "food");

            migrationBuilder.DropIndex(
                name: "IX_food_ExtendIdentityUserId",
                table: "food");

            migrationBuilder.DropColumn(
                name: "ExtendIdentityUserId",
                table: "food");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtendIdentityUserId",
                table: "food",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_ExtendIdentityUserId",
                table: "food",
                column: "ExtendIdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_food_AspNetUsers_ExtendIdentityUserId",
                table: "food",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
