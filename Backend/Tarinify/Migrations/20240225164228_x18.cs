using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlanMeal_meal_MealId",
                table: "nutritionPlanMeal");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlanMeal_MealId",
                table: "nutritionPlanMeal");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "nutritionPlanMeal");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailContentType",
                table: "meal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ThumbnailData",
                table: "meal",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailExtension",
                table: "meal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "nutritionPlanMealFood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    NutritionPlanMealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutritionPlanMealFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nutritionPlanMealFood_meal_MealId",
                        column: x => x.MealId,
                        principalTable: "meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_nutritionPlanMealFood_nutritionPlanMeal_NutritionPlanMealId",
                        column: x => x.NutritionPlanMealId,
                        principalTable: "nutritionPlanMeal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMealFood_MealId",
                table: "nutritionPlanMealFood",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMealFood_NutritionPlanMealId",
                table: "nutritionPlanMealFood",
                column: "NutritionPlanMealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nutritionPlanMealFood");

            migrationBuilder.DropColumn(
                name: "ThumbnailContentType",
                table: "meal");

            migrationBuilder.DropColumn(
                name: "ThumbnailData",
                table: "meal");

            migrationBuilder.DropColumn(
                name: "ThumbnailExtension",
                table: "meal");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "nutritionPlanMeal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMeal_MealId",
                table: "nutritionPlanMeal",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlanMeal_meal_MealId",
                table: "nutritionPlanMeal",
                column: "MealId",
                principalTable: "meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
