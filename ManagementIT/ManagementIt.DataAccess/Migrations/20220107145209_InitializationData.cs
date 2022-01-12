using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementIt.DataAccess.Migrations
{
    public partial class InitializationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationsPriority",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { 1, true, "Низкий" },
                    { 2, false, "Средний" },
                    { 3, false, "Высокий" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationsState",
                columns: new[] { "Id", "BGColor", "IsDefault", "Name", "State" },
                values: new object[,]
                {
                    { 1, "LightYellow", true, "Свободная", null },
                    { 2, "LimeGreen", false, "Выполняется", null },
                    { 3, "Salmon", false, "Выполнена", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationsPriority",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationsPriority",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationsPriority",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ApplicationsState",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationsState",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationsState",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
