using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCertificate_AspNetUsers_ExtendIdentityUserId",
                table: "TrainerCertificate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerCertificate",
                table: "TrainerCertificate");

            migrationBuilder.RenameTable(
                name: "TrainerCertificate",
                newName: "trainerCertificate");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerCertificate_ExtendIdentityUserId",
                table: "trainerCertificate",
                newName: "IX_trainerCertificate_ExtendIdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainerCertificate",
                table: "trainerCertificate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_trainerCertificate_AspNetUsers_ExtendIdentityUserId",
                table: "trainerCertificate",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainerCertificate_AspNetUsers_ExtendIdentityUserId",
                table: "trainerCertificate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainerCertificate",
                table: "trainerCertificate");

            migrationBuilder.RenameTable(
                name: "trainerCertificate",
                newName: "TrainerCertificate");

            migrationBuilder.RenameIndex(
                name: "IX_trainerCertificate_ExtendIdentityUserId",
                table: "TrainerCertificate",
                newName: "IX_TrainerCertificate_ExtendIdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerCertificate",
                table: "TrainerCertificate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCertificate_AspNetUsers_ExtendIdentityUserId",
                table: "TrainerCertificate",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
