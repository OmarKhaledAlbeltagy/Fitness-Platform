using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlanMealDay_nutritionPlanMeal_NutritionPlanMealId",
                table: "nutritionPlanMealDay");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlanMealDay_NutritionPlanMealId",
                table: "nutritionPlanMealDay");

            migrationBuilder.AddColumn<int>(
                name: "nutritionPlanId",
                table: "nutritionPlanMealDay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "nutritionPlan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "nutritionPlan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDateTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ClientNutritionPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutritionPlanId = table.Column<int>(type: "int", nullable: false),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientNutritionPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientNutritionPlan_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientNutritionPlan_nutritionPlan_NutritionPlanId",
                        column: x => x.NutritionPlanId,
                        principalTable: "nutritionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ClientWorkout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientWorkout_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientWorkout_workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMealDay_nutritionPlanId",
                table: "nutritionPlanMealDay",
                column: "nutritionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientNutritionPlan_ExtendIdentityUserId",
                table: "ClientNutritionPlan",
                column: "ExtendIdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientNutritionPlan_NutritionPlanId",
                table: "ClientNutritionPlan",
                column: "NutritionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientWorkout_ExtendIdentityUserId",
                table: "ClientWorkout",
                column: "ExtendIdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientWorkout_WorkoutId",
                table: "ClientWorkout",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlanMealDay_nutritionPlan_nutritionPlanId",
                table: "nutritionPlanMealDay",
                column: "nutritionPlanId",
                principalTable: "nutritionPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlanMealDay_nutritionPlan_nutritionPlanId",
                table: "nutritionPlanMealDay");

            migrationBuilder.DropTable(
                name: "ClientNutritionPlan");

            migrationBuilder.DropTable(
                name: "ClientWorkout");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlanMealDay_nutritionPlanId",
                table: "nutritionPlanMealDay");

            migrationBuilder.DropColumn(
                name: "nutritionPlanId",
                table: "nutritionPlanMealDay");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "RegistrationDateTime",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlanMealDay_NutritionPlanMealId",
                table: "nutritionPlanMealDay",
                column: "NutritionPlanMealId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlanMealDay_nutritionPlanMeal_NutritionPlanMealId",
                table: "nutritionPlanMealDay",
                column: "NutritionPlanMealId",
                principalTable: "nutritionPlanMeal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
