using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x49 : Migration
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainerTitleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "trainerTitle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainerTitle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainerTitleId",
                table: "AspNetUsers",
                column: "TrainerTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_country_PhoneCodeId",
                table: "AspNetUsers",
                column: "PhoneCodeId",
                principalTable: "country",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_trainerTitle_TrainerTitleId",
                table: "AspNetUsers",
                column: "TrainerTitleId",
                principalTable: "trainerTitle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_country_PhoneCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_trainerTitle_TrainerTitleId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "trainerTitle");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainerTitleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrainerTitleId",
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
