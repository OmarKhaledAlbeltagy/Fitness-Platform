using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x51 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ExtendIdentityUserId",
                table: "nutritionPlan");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlan_ExtendIdentityUserId",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "ExtendIdentityUserId",
                table: "nutritionPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtendIdentityUserId",
                table: "nutritionPlan",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlan_ExtendIdentityUserId",
                table: "nutritionPlan",
                column: "ExtendIdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ExtendIdentityUserId",
                table: "nutritionPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
