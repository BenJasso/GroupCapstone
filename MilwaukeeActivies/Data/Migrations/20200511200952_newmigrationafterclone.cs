using Microsoft.EntityFrameworkCore.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

namespace MilwaukeeActivies.Data.Migrations
{
    public partial class newmigrationafterclone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95043798-1b08-49b5-85a4-6ed2e08cd782");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49de4542-7080-4759-8b97-db3c93ed0357", "3234a89d-cc27-487e-88d7-4951db7eb528", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49de4542-7080-4759-8b97-db3c93ed0357");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "95043798-1b08-49b5-85a4-6ed2e08cd782", "cdc7e811-1f04-422c-8fa2-6a2960988c40", "Customer", "CUSTOMER" });
        }
    }
}
