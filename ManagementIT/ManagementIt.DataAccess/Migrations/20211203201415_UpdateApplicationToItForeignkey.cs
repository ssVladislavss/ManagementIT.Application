using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class UpdateApplicationToItForeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name:"FK_ApplicationsToIt_Employees_EmployeeId",
                table:"ApplicationsToIt");
            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationsToIt_Employees_EmployeeId",
                table: "ApplicationsToIt",
                column:"EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetDefault);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationsToIt_Employees_EmployeeId",
                table: "ApplicationsToIt");
            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationsToIt_Employees_EmployeeId",
                table: "ApplicationsToIt",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
