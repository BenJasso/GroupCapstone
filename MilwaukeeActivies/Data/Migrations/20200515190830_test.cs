using Microsoft.EntityFrameworkCore.Migrations;

namespace MilwaukeeActivies.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "587de64b-7eae-44b7-bb84-34c800367dcf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "edf8aa84-df8d-4cf6-a9c5-161e1bfdf7a9", "2da53992-0661-40c5-8920-62d80541f6e3", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edf8aa84-df8d-4cf6-a9c5-161e1bfdf7a9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "587de64b-7eae-44b7-bb84-34c800367dcf", "aea7b8c9-32c5-48c9-a20b-ba4834661741", "Customer", "CUSTOMER" });
        }
    }
}
