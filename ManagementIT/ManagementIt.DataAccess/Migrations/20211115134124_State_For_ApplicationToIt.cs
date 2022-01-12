using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class State_For_ApplicationToIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "ApplicationsToIt",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationsToIt_StateId",
                table: "ApplicationsToIt",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationsToIt_ApplicationsState_StateId",
                table: "ApplicationsToIt",
                column: "StateId",
                principalTable: "ApplicationsState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationsToIt_ApplicationsState_StateId",
                table: "ApplicationsToIt");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationsToIt_StateId",
                table: "ApplicationsToIt");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "ApplicationsToIt");
        }
    }
}
