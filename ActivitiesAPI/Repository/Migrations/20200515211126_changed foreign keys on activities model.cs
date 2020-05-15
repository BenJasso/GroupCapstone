using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class changedforeignkeysonactivitiesmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityTypeId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "ActivityTypes",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityName",
                table: "Activities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityTypes",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "ActivityTypeId",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
