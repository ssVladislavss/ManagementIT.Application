using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class EployeePhotoAddPropertyEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeePhotos_PhotoId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PhotoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeePhotos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePhotos_EmployeeId",
                table: "EmployeePhotos",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePhotos_Employees_EmployeeId",
                table: "EmployeePhotos",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePhotos_Employees_EmployeeId",
                table: "EmployeePhotos");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePhotos_EmployeeId",
                table: "EmployeePhotos");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeePhotos");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PhotoId",
                table: "Employees",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeePhotos_PhotoId",
                table: "Employees",
                column: "PhotoId",
                principalTable: "EmployeePhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
