using Microsoft.EntityFrameworkCore.Migrations;

namespace MilwaukeeActivies.Data.Migrations
{
    public partial class testingforactivtytypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edf8aa84-df8d-4cf6-a9c5-161e1bfdf7a9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99097acb-0cbf-4682-a2c3-9f21c017cf2d", "775f4212-5e64-4996-9094-9e5883d27cd5", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99097acb-0cbf-4682-a2c3-9f21c017cf2d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "edf8aa84-df8d-4cf6-a9c5-161e1bfdf7a9", "2da53992-0661-40c5-8920-62d80541f6e3", "Customer", "CUSTOMER" });
        }
    }
}
