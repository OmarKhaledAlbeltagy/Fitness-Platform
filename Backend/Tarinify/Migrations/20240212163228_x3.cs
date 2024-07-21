using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainify.Migrations
{
    /// <inheritdoc />
    public partial class x3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "ClientRegisteredPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientRegisteredPlan_TrainerPlan_TrainerPlanId",
                table: "ClientRegisteredPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerPlan_AspNetUsers_ExtendIdentityUserId",
                table: "TrainerPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "TrainerRegisteredPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerRegisteredPlan_PlatformPlan_PlatformPlanId",
                table: "TrainerRegisteredPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerRegisteredPlan",
                table: "TrainerRegisteredPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerPlan",
                table: "TrainerPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientRegisteredPlan",
                table: "ClientRegisteredPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlatformPlan",
                table: "PlatformPlan");

            migrationBuilder.RenameTable(
                name: "TrainerRegisteredPlan",
                newName: "trainerRegisteredPlan");

            migrationBuilder.RenameTable(
                name: "TrainerPlan",
                newName: "trainerPlan");

            migrationBuilder.RenameTable(
                name: "ClientRegisteredPlan",
                newName: "ClientRegisteredPlan");

            migrationBuilder.RenameTable(
                name: "PlatformPlan",
                newName: "platformPlan");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerRegisteredPlan_PlatformPlanId",
                table: "trainerRegisteredPlan",
                newName: "IX_trainerRegisteredPlan_PlatformPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerRegisteredPlan_ExtendIdentityUserId",
                table: "trainerRegisteredPlan",
                newName: "IX_trainerRegisteredPlan_ExtendIdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerPlan_ExtendIdentityUserId",
                table: "trainerPlan",
                newName: "IX_trainerPlan_ExtendIdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientRegisteredPlan_TrainerPlanId",
                table: "ClientRegisteredPlan",
                newName: "IX_ClientRegisteredPlan_TrainerPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientRegisteredPlan_ExtendIdentityUserId",
                table: "ClientRegisteredPlan",
                newName: "IX_ClientRegisteredPlan_ExtendIdentityUserId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureContentType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePictureData",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureExtension",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainerRegisteredPlan",
                table: "trainerRegisteredPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainerPlan",
                table: "trainerPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientRegisteredPlan",
                table: "ClientRegisteredPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_platformPlan",
                table: "platformPlan",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientRegisteredPlanId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientInvoice_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientInvoice_ClientRegisteredPlan_ClientRegisteredPlanId",
                        column: x => x.ClientRegisteredPlanId,
                        principalTable: "ClientRegisteredPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainerRegisteredPlanId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerInvoice_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainerInvoice_trainerRegisteredPlan_TrainerRegisteredPlanId",
                        column: x => x.TrainerRegisteredPlanId,
                        principalTable: "trainerRegisteredPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StateId",
                table: "AspNetUsers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInvoice_StateId",
                table: "ClientInvoice",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInvoice_ClientRegisteredPlanId",
                table: "ClientInvoice",
                column: "ClientRegisteredPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerInvoice_StateId",
                table: "TrainerInvoice",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerInvoice_TrainerRegisteredPlanId",
                table: "TrainerInvoice",
                column: "TrainerRegisteredPlanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "ClientRegisteredPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientRegisteredPlan_trainerPlan_TrainerPlanId",
                table: "ClientRegisteredPlan",
                column: "TrainerPlanId",
                principalTable: "trainerPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trainerPlan_AspNetUsers_ExtendIdentityUserId",
                table: "trainerPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_trainerRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "trainerRegisteredPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trainerRegisteredPlan_platformPlan_PlatformPlanId",
                table: "trainerRegisteredPlan",
                column: "PlatformPlanId",
                principalTable: "platformPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_State_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "ClientRegisteredPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientRegisteredPlan_trainerPlan_TrainerPlanId",
                table: "ClientRegisteredPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_trainerPlan_AspNetUsers_ExtendIdentityUserId",
                table: "trainerPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_trainerRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "trainerRegisteredPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_trainerRegisteredPlan_platformPlan_PlatformPlanId",
                table: "trainerRegisteredPlan");

            migrationBuilder.DropTable(
                name: "ClientInvoice");

            migrationBuilder.DropTable(
                name: "TrainerInvoice");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainerRegisteredPlan",
                table: "trainerRegisteredPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainerPlan",
                table: "trainerPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientRegisteredPlan",
                table: "ClientRegisteredPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_platformPlan",
                table: "platformPlan");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureContentType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureData",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureExtension",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "trainerRegisteredPlan",
                newName: "TrainerRegisteredPlan");

            migrationBuilder.RenameTable(
                name: "trainerPlan",
                newName: "TrainerPlan");

            migrationBuilder.RenameTable(
                name: "ClientRegisteredPlan",
                newName: "ClientRegisteredPlan");

            migrationBuilder.RenameTable(
                name: "platformPlan",
                newName: "PlatformPlan");

            migrationBuilder.RenameIndex(
                name: "IX_trainerRegisteredPlan_PlatformPlanId",
                table: "TrainerRegisteredPlan",
                newName: "IX_TrainerRegisteredPlan_PlatformPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_trainerRegisteredPlan_ExtendIdentityUserId",
                table: "TrainerRegisteredPlan",
                newName: "IX_TrainerRegisteredPlan_ExtendIdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_trainerPlan_ExtendIdentityUserId",
                table: "TrainerPlan",
                newName: "IX_TrainerPlan_ExtendIdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientRegisteredPlan_TrainerPlanId",
                table: "ClientRegisteredPlan",
                newName: "IX_ClientRegisteredPlan_TrainerPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientRegisteredPlan_ExtendIdentityUserId",
                table: "ClientRegisteredPlan",
                newName: "IX_ClientRegisteredPlan_ExtendIdentityUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerRegisteredPlan",
                table: "TrainerRegisteredPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerPlan",
                table: "TrainerPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientRegisteredPlan",
                table: "ClientRegisteredPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlatformPlan",
                table: "PlatformPlan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "ClientRegisteredPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientRegisteredPlan_TrainerPlan_TrainerPlanId",
                table: "ClientRegisteredPlan",
                column: "TrainerPlanId",
                principalTable: "TrainerPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerPlan_AspNetUsers_ExtendIdentityUserId",
                table: "TrainerPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerRegisteredPlan_AspNetUsers_ExtendIdentityUserId",
                table: "TrainerRegisteredPlan",
                column: "ExtendIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerRegisteredPlan_PlatformPlan_PlatformPlanId",
                table: "TrainerRegisteredPlan",
                column: "PlatformPlanId",
                principalTable: "PlatformPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
