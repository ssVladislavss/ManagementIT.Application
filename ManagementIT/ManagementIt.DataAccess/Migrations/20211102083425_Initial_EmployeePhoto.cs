using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class Initial_EmployeePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePhotos", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeePhotos_PhotoId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeePhotos");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PhotoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Employees",
                type: "text",
                nullable: true);
        }
    }
}
