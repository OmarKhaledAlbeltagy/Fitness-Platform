using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guidd",
                table: "workout",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "Guidd",
                table: "exersice",
                newName: "Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "workout",
                newName: "Guidd");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "exersice",
                newName: "Guidd");
        }
    }
}
