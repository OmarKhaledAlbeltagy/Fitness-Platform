using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x52 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "nutritionPlan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WeightGoalId",
                table: "nutritionPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "Protein",
                table: "food",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Fats",
                table: "food",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Carb",
                table: "food",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Calories",
                table: "food",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "ActivityLevelValue",
                table: "activityLevel",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "notCompletedSignup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    ExtendIdentityRoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notCompletedSignup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notCompletedSignup_AspNetRoles_ExtendIdentityRoleId",
                        column: x => x.ExtendIdentityRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notCompletedSignup_state_StateId",
                        column: x => x.StateId,
                        principalTable: "state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlan_WeightGoalId",
                table: "nutritionPlan",
                column: "WeightGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_notCompletedSignup_ExtendIdentityRoleId",
                table: "notCompletedSignup",
                column: "ExtendIdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_notCompletedSignup_StateId",
                table: "notCompletedSignup",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlan_weightGoal_WeightGoalId",
                table: "nutritionPlan",
                column: "WeightGoalId",
                principalTable: "weightGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlan_weightGoal_WeightGoalId",
                table: "nutritionPlan");

            migrationBuilder.DropTable(
                name: "notCompletedSignup");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlan_WeightGoalId",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "WeightGoalId",
                table: "nutritionPlan");

            migrationBuilder.AlterColumn<double>(
                name: "Protein",
                table: "food",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Fats",
                table: "food",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Carb",
                table: "food",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Calories",
                table: "food",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivityLevelValue",
                table: "activityLevel",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
