using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class addPropertyIniciatorOrApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IniciatorFullName",
                table: "ApplicationsToIt",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameIniciator",
                table: "ApplicationsToIt",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IniciatorFullName",
                table: "ApplicationsToIt");

            migrationBuilder.DropColumn(
                name: "UserNameIniciator",
                table: "ApplicationsToIt");
        }
    }
}
