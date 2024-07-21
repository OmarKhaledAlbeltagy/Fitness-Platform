using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x50 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ExtendIdentityUserId",
                table: "nutritionPlan");

            migrationBuilder.DropTable(
                name: "ClientNutritionPlan");

            migrationBuilder.AlterColumn<string>(
                name: "ExtendIdentityUserId",
                table: "nutritionPlan",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "nutritionPlan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "nutritionPlan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "nutritionPlan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainerId",
                table: "nutritionPlan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlan_ClientId",
                table: "nutritionPlan",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlan_TrainerId",
                table: "nutritionPlan",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ClientId",
                table: "nutritionPlan",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ExtendIdentityUserId",
                table: "nutritionPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_TrainerId",
                table: "nutritionPlan",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ClientId",
                table: "nutritionPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ExtendIdentityUserId",
                table: "nutritionPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_TrainerId",
                table: "nutritionPlan");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlan_ClientId",
                table: "nutritionPlan");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlan_TrainerId",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "End",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "nutritionPlan");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "nutritionPlan");

            migrationBuilder.AlterColumn<string>(
                name: "ExtendIdentityUserId",
                table: "nutritionPlan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClientNutritionPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NutritionPlanId = table.Column<int>(type: "int", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientNutritionPlan_ExtendIdentityUserId",
                table: "ClientNutritionPlan",
                column: "ExtendIdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientNutritionPlan_NutritionPlanId",
                table: "ClientNutritionPlan",
                column: "NutritionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlan_AspNetUsers_ExtendIdentityUserId",
                table: "nutritionPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
