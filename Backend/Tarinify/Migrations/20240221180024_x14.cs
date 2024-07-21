using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "meal");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "meal");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "meal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QuantityType",
                table: "meal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "meal");

            migrationBuilder.DropColumn(
                name: "QuantityType",
                table: "meal");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "meal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "meal",
                type: "int",
                nullable: true);
        }
    }
}
