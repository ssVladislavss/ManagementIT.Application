using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class OnDeleteForApplicationToIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnDelete",
                table: "ApplicationsToIt",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnDelete",
                table: "ApplicationsToIt");
        }
    }
}
