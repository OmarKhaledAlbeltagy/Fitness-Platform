using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "nutritionPlanMeal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Carb",
                table: "meal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fats",
                table: "meal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "meal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Protein",
                table: "meal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlatformPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Months = table.Column<int>(type: "int", nullable: false),
                    ClientLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainerPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Months = table.Column<int>(type: "int", nullable: false),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerPlan_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerRegisteredPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlatformPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerRegisteredPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerRegisteredPlan_PlatformPlan_PlatformPlanId",
                        column: x => x.PlatformPlanId,
                        principalTable: "PlatformPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRegisteredPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrainerPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRegisteredPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRegisteredPlan_TrainerPlan_TrainerPlanId",
                        column: x => x.TrainerPlanId,
                        principalTable: "TrainerPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientRegisteredPlan_ExtendIdentityUserId",
                table: "ClientRegisteredPlan",
                column: "ExtendIdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRegisteredPlan_TrainerPlanId",
                table: "ClientRegisteredPlan",
                column: "TrainerPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPlan_ExtendIdentityUserId",
                table: "TrainerPlan",
                column: "ExtendIdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRegisteredPlan_ExtendIdentityUserId",
                table: "TrainerRegisteredPlan",
                column: "ExtendIdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRegisteredPlan_PlatformPlanId",
                table: "TrainerRegisteredPlan",
                column: "PlatformPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRegisteredPlan");

            migrationBuilder.DropTable(
                name: "TrainerRegisteredPlan");

            migrationBuilder.DropTable(
                name: "TrainerPlan");

            migrationBuilder.DropTable(
                name: "PlatformPlan");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "nutritionPlanMeal");

            migrationBuilder.DropColumn(
                name: "Carb",
                table: "meal");

            migrationBuilder.DropColumn(
                name: "Fats",
                table: "meal");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "meal");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "meal");
        }
    }
}
