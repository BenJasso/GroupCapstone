using Microsoft.EntityFrameworkCore.Migrations;

namespace MilwaukeeActivies.Data.Migrations
{
    public partial class Initialmigrationofmodelstodatabasefortables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab368ab7-e19b-44e0-8d12-ec67605dc918");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa0c26d3-f334-4290-acf7-2f943d2ab410", "e7030c63-d0fc-48f9-a0ab-3511d8e14fb0", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa0c26d3-f334-4290-acf7-2f943d2ab410");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab368ab7-e19b-44e0-8d12-ec67605dc918", "2eecf1dd-5bf8-43bf-85d1-175a7ece5b1c", "Customer", "CUSTOMER" });
        }
    }
}
