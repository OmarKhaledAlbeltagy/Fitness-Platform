using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x39 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_food_foodCategory_FoodCategoryId",
                table: "food");

            migrationBuilder.AlterColumn<int>(
                name: "FoodCategoryId",
                table: "food",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_food_foodCategory_FoodCategoryId",
                table: "food",
                column: "FoodCategoryId",
                principalTable: "foodCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_food_foodCategory_FoodCategoryId",
                table: "food");

            migrationBuilder.AlterColumn<int>(
                name: "FoodCategoryId",
                table: "food",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_food_foodCategory_FoodCategoryId",
                table: "food",
                column: "FoodCategoryId",
                principalTable: "foodCategory",
                principalColumn: "Id");
        }
    }
}
