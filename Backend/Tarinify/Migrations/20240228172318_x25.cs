using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_country_PhoneCodeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneCodeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_country_PhoneCodeId",
                table: "AspNetUsers",
                column: "PhoneCodeId",
                principalTable: "country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_country_PhoneCodeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneCodeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_country_PhoneCodeId",
                table: "AspNetUsers",
                column: "PhoneCodeId",
                principalTable: "country",
                principalColumn: "Id");
        }
    }
}
