using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x46 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ActivityLevel_ActivityLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BodyType_BodyTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WeightGoal_WeightGoalId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightGoal",
                table: "WeightGoal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyType",
                table: "BodyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLevel",
                table: "ActivityLevel");

            migrationBuilder.RenameTable(
                name: "WeightGoal",
                newName: "weightGoal");

            migrationBuilder.RenameTable(
                name: "BodyType",
                newName: "bodyType");

            migrationBuilder.RenameTable(
                name: "ActivityLevel",
                newName: "activityLevel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_weightGoal",
                table: "weightGoal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bodyType",
                table: "bodyType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_activityLevel",
                table: "activityLevel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_activityLevel_ActivityLevelId",
                table: "AspNetUsers",
                column: "ActivityLevelId",
                principalTable: "activityLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_bodyType_BodyTypeId",
                table: "AspNetUsers",
                column: "BodyTypeId",
                principalTable: "bodyType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_weightGoal_WeightGoalId",
                table: "AspNetUsers",
                column: "WeightGoalId",
                principalTable: "weightGoal",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_activityLevel_ActivityLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_bodyType_BodyTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_weightGoal_WeightGoalId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_weightGoal",
                table: "weightGoal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bodyType",
                table: "bodyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_activityLevel",
                table: "activityLevel");

            migrationBuilder.RenameTable(
                name: "weightGoal",
                newName: "WeightGoal");

            migrationBuilder.RenameTable(
                name: "bodyType",
                newName: "BodyType");

            migrationBuilder.RenameTable(
                name: "activityLevel",
                newName: "ActivityLevel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightGoal",
                table: "WeightGoal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyType",
                table: "BodyType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLevel",
                table: "ActivityLevel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ActivityLevel_ActivityLevelId",
                table: "AspNetUsers",
                column: "ActivityLevelId",
                principalTable: "ActivityLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BodyType_BodyTypeId",
                table: "AspNetUsers",
                column: "BodyTypeId",
                principalTable: "BodyType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WeightGoal_WeightGoalId",
                table: "AspNetUsers",
                column: "WeightGoalId",
                principalTable: "WeightGoal",
                principalColumn: "Id");
        }
    }
}
