using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "nutritionPlanMeal");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "nutritionPlanMealFood",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "nutritionPlanMealFood");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "nutritionPlanMeal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
