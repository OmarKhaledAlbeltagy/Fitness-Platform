using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlanMeal_mealType_MealTypeId",
                table: "nutritionPlanMeal");

            migrationBuilder.DropTable(
                name: "mealType");

            migrationBuilder.DropTable(
                name: "nutritionPlanMealDay");

            migrationBuilder.DropTable(
                name: "week");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlanMeal_MealTypeId",
                table: "nutritionPlanMeal");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "nutritionPlanMeal");

            migrationBuilder.AddColumn<DateTime>(
                name: "MealTime",
                table: "nutritionPlanMeal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "nutritionPlanMeal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealTime",
                table: "nutritionPlanMeal");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "nutritionPlanMeal");

            migrationBuilder.AddColumn<int>(
                name: "MealTypeId",
                table: "nutritionPlanMeal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "mealType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mealType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "week",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_week", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nutritionPlanMealDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nutritionPlanId = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    NutritionPlanMealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutritionPlanMealDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nutritionPlanMealDay_nutritionPlan_nutritionPlanId",
                        column: x => x.nutritionPlanId,
                        principalTable: "nutritionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_nutritionPlanMealDay_week_WeekId",
                        column: x => x.WeekId,
                        principalTable: "week",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMeal_MealTypeId",
                table: "nutritionPlanMeal",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMealDay_nutritionPlanId",
                table: "nutritionPlanMealDay",
                column: "nutritionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMealDay_WeekId",
                table: "nutritionPlanMealDay",
                column: "WeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlanMeal_mealType_MealTypeId",
                table: "nutritionPlanMeal",
                column: "MealTypeId",
                principalTable: "mealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
