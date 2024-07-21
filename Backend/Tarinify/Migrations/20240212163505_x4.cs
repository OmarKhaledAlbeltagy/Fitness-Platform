using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_State_Country_CountryId",
                table: "State");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInvoice_State_StateId",
                table: "ClientInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInvoice_ClientRegisteredPlan_ClientRegisteredPlanId",
                table: "ClientInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerInvoice_State_StateId",
                table: "TrainerInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerInvoice_trainerRegisteredPlan_TrainerRegisteredPlanId",
                table: "TrainerInvoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerInvoice",
                table: "TrainerInvoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientInvoice",
                table: "ClientInvoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_State",
                table: "State");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "TrainerInvoice",
                newName: "trainerInvoice");

            migrationBuilder.RenameTable(
                name: "ClientInvoice",
                newName: "ClientInvoice");

            migrationBuilder.RenameTable(
                name: "State",
                newName: "state");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "country");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerInvoice_TrainerRegisteredPlanId",
                table: "trainerInvoice",
                newName: "IX_trainerInvoice_TrainerRegisteredPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerInvoice_StateId",
                table: "trainerInvoice",
                newName: "IX_trainerInvoice_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientInvoice_ClientRegisteredPlanId",
                table: "ClientInvoice",
                newName: "IX_ClientInvoice_ClientRegisteredPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientInvoice_StateId",
                table: "ClientInvoice",
                newName: "IX_ClientInvoice_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_State_CountryId",
                table: "state",
                newName: "IX_state_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainerInvoice",
                table: "trainerInvoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientInvoice",
                table: "ClientInvoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_state",
                table: "state",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_country",
                table: "country",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_ExtendIdentityUserId",
                table: "post",
                column: "ExtendIdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_state_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_state_country_CountryId",
                table: "state",
                column: "CountryId",
                principalTable: "country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInvoice_state_StateId",
                table: "ClientInvoice",
                column: "StateId",
                principalTable: "state",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInvoice_ClientRegisteredPlan_ClientRegisteredPlanId",
                table: "ClientInvoice",
                column: "ClientRegisteredPlanId",
                principalTable: "ClientRegisteredPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trainerInvoice_state_StateId",
                table: "trainerInvoice",
                column: "StateId",
                principalTable: "state",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_trainerInvoice_trainerRegisteredPlan_TrainerRegisteredPlanId",
                table: "trainerInvoice",
                column: "TrainerRegisteredPlanId",
                principalTable: "trainerRegisteredPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_state_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_state_country_CountryId",
                table: "state");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInvoice_state_StateId",
                table: "ClientInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInvoice_ClientRegisteredPlan_ClientRegisteredPlanId",
                table: "ClientInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_trainerInvoice_state_StateId",
                table: "trainerInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_trainerInvoice_trainerRegisteredPlan_TrainerRegisteredPlanId",
                table: "trainerInvoice");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainerInvoice",
                table: "trainerInvoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientInvoice",
                table: "ClientInvoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_state",
                table: "state");

            migrationBuilder.DropPrimaryKey(
                name: "PK_country",
                table: "country");

            migrationBuilder.RenameTable(
                name: "trainerInvoice",
                newName: "TrainerInvoice");

            migrationBuilder.RenameTable(
                name: "ClientInvoice",
                newName: "ClientInvoice");

            migrationBuilder.RenameTable(
                name: "state",
                newName: "State");

            migrationBuilder.RenameTable(
                name: "country",
                newName: "Country");

            migrationBuilder.RenameIndex(
                name: "IX_trainerInvoice_TrainerRegisteredPlanId",
                table: "TrainerInvoice",
                newName: "IX_TrainerInvoice_TrainerRegisteredPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_trainerInvoice_StateId",
                table: "TrainerInvoice",
                newName: "IX_TrainerInvoice_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientInvoice_ClientRegisteredPlanId",
                table: "ClientInvoice",
                newName: "IX_ClientInvoice_ClientRegisteredPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientInvoice_StateId",
                table: "ClientInvoice",
                newName: "IX_ClientInvoice_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_state_CountryId",
                table: "State",
                newName: "IX_State_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerInvoice",
                table: "TrainerInvoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientInvoice",
                table: "ClientInvoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_State",
                table: "State",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_State_Country_CountryId",
                table: "State",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInvoice_State_StateId",
                table: "ClientInvoice",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInvoice_ClientRegisteredPlan_ClientRegisteredPlanId",
                table: "ClientInvoice",
                column: "ClientRegisteredPlanId",
                principalTable: "ClientRegisteredPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerInvoice_State_StateId",
                table: "TrainerInvoice",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerInvoice_trainerRegisteredPlan_TrainerRegisteredPlanId",
                table: "TrainerInvoice",
                column: "TrainerRegisteredPlanId",
                principalTable: "trainerRegisteredPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
