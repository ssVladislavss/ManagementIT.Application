using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class UpdateApplicationAndAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserNameIniciator",
                table: "ApplicationsToIt");

            migrationBuilder.RenameColumn(
                name: "UserNameIniciator",
                table: "ApplicationsAction",
                newName: "IniciatorFullName");

            migrationBuilder.AddColumn<int>(
                name: "IniciatorId",
                table: "ApplicationsToIt",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IniciatorId",
                table: "ApplicationsAction",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IniciatorId",
                table: "ApplicationsToIt");

            migrationBuilder.DropColumn(
                name: "IniciatorId",
                table: "ApplicationsAction");

            migrationBuilder.RenameColumn(
                name: "IniciatorFullName",
                table: "ApplicationsAction",
                newName: "UserNameIniciator");

            migrationBuilder.AddColumn<string>(
                name: "UserNameIniciator",
                table: "ApplicationsToIt",
                type: "text",
                nullable: true);
        }
    }
}
