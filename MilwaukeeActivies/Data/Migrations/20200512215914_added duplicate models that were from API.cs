using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MilwaukeeActivies.Data.Migrations
{
    public partial class addedduplicatemodelsthatwerefromAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49de4542-7080-4759-8b97-db3c93ed0357");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EventName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    SiteURL = table.Column<string>(nullable: true),
                    Season = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Indoor = table.Column<bool>(nullable: false),
                    ActivityTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    ActivityTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityTypes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.ActivityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(nullable: true),
                    ActivityId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ActivitiesActivityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Activities_ActivitiesActivityId",
                        column: x => x.ActivitiesActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc22e588-6d7e-4397-9ca7-943abf8dfbbf", "21b3bffc-cc40-4b0e-a5da-fd0cf1fadd42", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ActivitiesActivityId",
                table: "Reviews",
                column: "ActivitiesActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc22e588-6d7e-4397-9ca7-943abf8dfbbf");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49de4542-7080-4759-8b97-db3c93ed0357", "3234a89d-cc27-487e-88d7-4951db7eb528", "Customer", "CUSTOMER" });
        }
    }
}
