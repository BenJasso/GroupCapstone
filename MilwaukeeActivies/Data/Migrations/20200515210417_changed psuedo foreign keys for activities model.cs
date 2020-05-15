using Microsoft.EntityFrameworkCore.Migrations;

namespace MilwaukeeActivies.Data.Migrations
{
    public partial class changedpsuedoforeignkeysforactivitiesmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99097acb-0cbf-4682-a2c3-9f21c017cf2d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8bf792a0-fc78-4727-836a-8983c6489062", "0fd8e3e4-7c8d-4cf3-9967-bee78e2bdd2c", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bf792a0-fc78-4727-836a-8983c6489062");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99097acb-0cbf-4682-a2c3-9f21c017cf2d", "775f4212-5e64-4996-9094-9e5883d27cd5", "Customer", "CUSTOMER" });
        }
    }
}
