using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x43 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_foodMealTypes_mealTypes_mealtypesId",
                table: "foodMealTypes");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "foodMealTypes");

            migrationBuilder.RenameColumn(
                name: "mealtypesId",
                table: "foodMealTypes",
                newName: "MealTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_foodMealTypes_mealtypesId",
                table: "foodMealTypes",
                newName: "IX_foodMealTypes_MealTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_foodMealTypes_mealTypes_MealTypesId",
                table: "foodMealTypes",
                column: "MealTypesId",
                principalTable: "mealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_foodMealTypes_mealTypes_MealTypesId",
                table: "foodMealTypes");

            migrationBuilder.RenameColumn(
                name: "MealTypesId",
                table: "foodMealTypes",
                newName: "mealtypesId");

            migrationBuilder.RenameIndex(
                name: "IX_foodMealTypes_MealTypesId",
                table: "foodMealTypes",
                newName: "IX_foodMealTypes_mealtypesId");

            migrationBuilder.AddColumn<int>(
                name: "MealTypeId",
                table: "foodMealTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_foodMealTypes_mealTypes_mealtypesId",
                table: "foodMealTypes",
                column: "mealtypesId",
                principalTable: "mealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
