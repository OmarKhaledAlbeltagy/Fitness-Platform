using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailContentType",
                table: "foodCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ThumbnailData",
                table: "foodCategory",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailExtension",
                table: "foodCategory",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailContentType",
                table: "foodCategory");

            migrationBuilder.DropColumn(
                name: "ThumbnailData",
                table: "foodCategory");

            migrationBuilder.DropColumn(
                name: "ThumbnailExtension",
                table: "foodCategory");
        }
    }
}
