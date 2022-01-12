﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class AddPropertyIsDefaultOrState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ApplicationsState",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ApplicationsState");
        }
    }
}
