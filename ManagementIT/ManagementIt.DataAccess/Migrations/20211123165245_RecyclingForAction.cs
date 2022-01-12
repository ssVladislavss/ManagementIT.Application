using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class RecyclingForAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationsAction_ApplicationsState_StateId",
                table: "ApplicationsAction");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationsAction_ApplicationsToIt_ApplicationToItId",
                table: "ApplicationsAction");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationsAction_Employees_InitiatorEmployeeId",
                table: "ApplicationsAction");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationsAction_ApplicationToItId",
                table: "ApplicationsAction");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationsAction_InitiatorEmployeeId",
                table: "ApplicationsAction");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationsAction_StateId",
                table: "ApplicationsAction");

            migrationBuilder.DropColumn(
                name: "ApplicationToItId",
                table: "ApplicationsAction");

            migrationBuilder.DropColumn(
                name: "InitiatorEmployeeId",
                table: "ApplicationsAction");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "ApplicationsAction");

            migrationBuilder.AddColumn<int>(
                name: "AppId",
                table: "ApplicationsAction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ApplicationsAction",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "ApplicationsAction",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullNameIniciator",
                table: "ApplicationsAction",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "ApplicationsAction",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppId",
                table: "ApplicationsAction");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ApplicationsAction");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "ApplicationsAction");

            migrationBuilder.DropColumn(
                name: "FullNameIniciator",
                table: "ApplicationsAction");

            migrationBuilder.DropColumn(
                name: "StateName",
                table: "ApplicationsAction");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationToItId",
                table: "ApplicationsAction",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitiatorEmployeeId",
                table: "ApplicationsAction",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "ApplicationsAction",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationsAction_ApplicationToItId",
                table: "ApplicationsAction",
                column: "ApplicationToItId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationsAction_InitiatorEmployeeId",
                table: "ApplicationsAction",
                column: "InitiatorEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationsAction_StateId",
                table: "ApplicationsAction",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationsAction_ApplicationsState_StateId",
                table: "ApplicationsAction",
                column: "StateId",
                principalTable: "ApplicationsState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationsAction_ApplicationsToIt_ApplicationToItId",
                table: "ApplicationsAction",
                column: "ApplicationToItId",
                principalTable: "ApplicationsToIt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationsAction_Employees_InitiatorEmployeeId",
                table: "ApplicationsAction",
                column: "InitiatorEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
