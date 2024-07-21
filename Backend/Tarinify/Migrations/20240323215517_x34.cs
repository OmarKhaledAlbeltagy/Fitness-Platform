using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "trainerPlan");

            migrationBuilder.RenameColumn(
                name: "Months",
                table: "trainerPlan",
                newName: "TrainersPlansDurationId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "Months");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "trainerPlan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
