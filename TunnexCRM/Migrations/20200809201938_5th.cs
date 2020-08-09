using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class _5th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleID",
                table: "Waybills");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "Waybills",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "Waybills");

            migrationBuilder.AddColumn<int>(
                name: "SaleID",
                table: "Waybills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
