using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortBio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortBio",
                table: "AspNetUsers");
        }
    }
}
