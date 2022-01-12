using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class ChangeApplicationAndAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "ApplicationsToIt",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeFullName",
                table: "ApplicationsToIt",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "ApplicationsToIt",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "ApplicationsAction",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "ApplicationsToIt");

            migrationBuilder.DropColumn(
                name: "EmployeeFullName",
                table: "ApplicationsToIt");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "ApplicationsToIt");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "ApplicationsAction");
        }
    }
}
