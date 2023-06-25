using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class ninAndPensionAddedToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nin",
                table: "Staffs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pension",
                table: "Staffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nin",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "pension",
                table: "Staffs");
        }
    }
}
