using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GalleryImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    GalleryImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GalleryImageContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userGallery_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userGallery_ExtendIdentityUserId",
                table: "userGallery",
                column: "ExtendIdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userGallery");
        }
    }
}
