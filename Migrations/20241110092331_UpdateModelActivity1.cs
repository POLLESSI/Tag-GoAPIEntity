using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApi.Migrations
{
    public partial class UpdateModelActivity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "NbRegistrationsMax",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "ColorRef",
                table: "Activities",
                newName: "AdditionalInformation");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Activities",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Activities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "AdditionalInformation",
                table: "Activities",
                newName: "ColorRef");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Activities",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbRegistrationsMax",
                table: "Activities",
                type: "int",
                nullable: true);
        }
    }
}
