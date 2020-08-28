using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class STringifyInvoiceNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Purchases");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "Purchases",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TransactionType",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceNo",
                table: "Purchases",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TransactionType",
                table: "Purchases",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
