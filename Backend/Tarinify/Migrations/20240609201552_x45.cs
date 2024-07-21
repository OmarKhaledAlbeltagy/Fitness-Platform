using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainerPlan_trainersPlansDuration_TrainersPlansDurationId",
                table: "trainerPlan");

            migrationBuilder.DropTable(
                name: "trainersPlansDuration");

            migrationBuilder.DropIndex(
                name: "IX_trainerPlan_TrainersPlansDurationId",
                table: "trainerPlan");

            migrationBuilder.RenameColumn(
                name: "TrainersPlansDurationId",
                table: "trainerPlan",
                newName: "DurationInDays");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivityLevelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BodyTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "HeightCM",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeightGoalId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WeightKG",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityLevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityLevelValue = table.Column<double>(type: "float", nullable: false),
                    ThumbnailData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ThumbnailExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ThumbnailExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightGoal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightGoal", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ActivityLevelId",
                table: "AspNetUsers",
                column: "ActivityLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BodyTypeId",
                table: "AspNetUsers",
                column: "BodyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WeightGoalId",
                table: "AspNetUsers",
                column: "WeightGoalId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "ActivityLevel");

            migrationBuilder.DropTable(
                name: "BodyType");

            migrationBuilder.DropTable(
                name: "WeightGoal");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ActivityLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BodyTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WeightGoalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActivityLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BodyTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HeightCM",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WeightGoalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WeightKG",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DurationInDays",
                table: "trainerPlan",
                newName: "TrainersPlansDurationId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "trainersPlansDuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Months = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainersPlansDuration", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trainerPlan_TrainersPlansDurationId",
                table: "trainerPlan",
                column: "TrainersPlansDurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_trainerPlan_trainersPlansDuration_TrainersPlansDurationId",
                table: "trainerPlan",
                column: "TrainersPlansDurationId",
                principalTable: "trainersPlansDuration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
