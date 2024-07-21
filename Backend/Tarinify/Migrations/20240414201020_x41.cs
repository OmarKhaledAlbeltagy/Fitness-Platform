using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mealTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mealTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "foodMealTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    MealTypeId = table.Column<int>(type: "int", nullable: false),
                    mealtypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodMealTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_foodMealTypes_food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_foodMealTypes_mealTypes_mealtypesId",
                        column: x => x.mealtypesId,
                        principalTable: "mealTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_foodMealTypes_FoodId",
                table: "foodMealTypes",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_foodMealTypes_mealtypesId",
                table: "foodMealTypes",
                column: "mealtypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "foodMealTypes");

            migrationBuilder.DropTable(
                name: "mealTypes");
        }
    }
}
