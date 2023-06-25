using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class dateAddedtoStaffskillorkpi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "StaffSkillorKPIs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "StaffSkillorKPIs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserCreated",
                table: "StaffSkillorKPIs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserModified",
                table: "StaffSkillorKPIs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "StaffSkillorKPIs");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "StaffSkillorKPIs");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "StaffSkillorKPIs");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "StaffSkillorKPIs");
        }
    }
}
