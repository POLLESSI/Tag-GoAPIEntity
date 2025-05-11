using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApi.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntityUserEntity_Activities_OrganizedActivitiesId",
                table: "ActivityEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntityUserEntity_Users_OrganizersId",
                table: "ActivityEntityUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityEntityUserEntity",
                table: "ActivityEntityUserEntity");

            migrationBuilder.RenameTable(
                name: "ActivityEntityUserEntity",
                newName: "ActivityOrganizers");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityEntityUserEntity_OrganizersId",
                table: "ActivityOrganizers",
                newName: "IX_ActivityOrganizers_OrganizersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityOrganizers",
                table: "ActivityOrganizers",
                columns: new[] { "OrganizedActivitiesId", "OrganizersId" });

            migrationBuilder.CreateTable(
                name: "ActivityRegistrations",
                columns: table => new
                {
                    RegisteredActivitiesId = table.Column<int>(type: "int", nullable: false),
                    RegisteredsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityRegistrations", x => new { x.RegisteredActivitiesId, x.RegisteredsId });
                    table.ForeignKey(
                        name: "FK_ActivityRegistrations_Activities_RegisteredActivitiesId",
                        column: x => x.RegisteredActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityRegistrations_Users_RegisteredsId",
                        column: x => x.RegisteredsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityRegistrations_RegisteredsId",
                table: "ActivityRegistrations",
                column: "RegisteredsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityOrganizers_Activities_OrganizedActivitiesId",
                table: "ActivityOrganizers",
                column: "OrganizedActivitiesId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityOrganizers_Users_OrganizersId",
                table: "ActivityOrganizers",
                column: "OrganizersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityOrganizers_Activities_OrganizedActivitiesId",
                table: "ActivityOrganizers");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityOrganizers_Users_OrganizersId",
                table: "ActivityOrganizers");

            migrationBuilder.DropTable(
                name: "ActivityRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityOrganizers",
                table: "ActivityOrganizers");

            migrationBuilder.RenameTable(
                name: "ActivityOrganizers",
                newName: "ActivityEntityUserEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityOrganizers_OrganizersId",
                table: "ActivityEntityUserEntity",
                newName: "IX_ActivityEntityUserEntity_OrganizersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityEntityUserEntity",
                table: "ActivityEntityUserEntity",
                columns: new[] { "OrganizedActivitiesId", "OrganizersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntityUserEntity_Activities_OrganizedActivitiesId",
                table: "ActivityEntityUserEntity",
                column: "OrganizedActivitiesId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntityUserEntity_Users_OrganizersId",
                table: "ActivityEntityUserEntity",
                column: "OrganizersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
