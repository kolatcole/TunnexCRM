using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class discountPercent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Invoices",
                newName: "DiscountPercent");

            migrationBuilder.AddColumn<string>(
                name: "LPO",
                table: "Sales",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LPO",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "DiscountPercent",
                table: "Invoices",
                newName: "Discount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Carts",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
