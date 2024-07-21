using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x35 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlanMealFood_meal_MealId",
                table: "nutritionPlanMealFood");

            migrationBuilder.DropTable(
                name: "meal");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "nutritionPlanMealFood",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_nutritionPlanMealFood_MealId",
                table: "nutritionPlanMealFood",
                newName: "IX_nutritionPlanMealFood_FoodId");

            migrationBuilder.CreateTable(
                name: "foodCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Fats = table.Column<int>(type: "int", nullable: false),
                    Carb = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ThumbnailData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ThumbnailExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FoodCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_food_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_food_foodCategory_FoodCategoryId",
                        column: x => x.FoodCategoryId,
                        principalTable: "foodCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "foodTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_foodTags_food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_food_ExtendIdentityUserId",
                table: "food",
                column: "ExtendIdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_food_FoodCategoryId",
                table: "food",
                column: "FoodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_foodTags_FoodId",
                table: "foodTags",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlanMealFood_food_FoodId",
                table: "nutritionPlanMealFood",
                column: "FoodId",
                principalTable: "food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlanMealFood_food_FoodId",
                table: "nutritionPlanMealFood");

            migrationBuilder.DropTable(
                name: "foodTags");

            migrationBuilder.DropTable(
                name: "food");

            migrationBuilder.DropTable(
                name: "foodCategory");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "nutritionPlanMealFood",
                newName: "MealId");

            migrationBuilder.RenameIndex(
                name: "IX_nutritionPlanMealFood_FoodId",
                table: "nutritionPlanMealFood",
                newName: "IX_nutritionPlanMealFood_MealId");

            migrationBuilder.CreateTable(
                name: "meal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Carb = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fats = table.Column<int>(type: "int", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ThumbnailExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_meal_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meal_ExtendIdentityUserId",
                table: "meal",
                column: "ExtendIdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlanMealFood_meal_MealId",
                table: "nutritionPlanMealFood",
                column: "MealId",
                principalTable: "meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
