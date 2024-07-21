using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x47 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GoalValue",
                table: "weightGoal",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalValue",
                table: "weightGoal");
        }
    }
}
