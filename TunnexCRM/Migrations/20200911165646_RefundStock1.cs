using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class RefundStock1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefundStock_Carts_CartID",
                table: "RefundStock");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RefundStock_RefundStockID",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefundStock",
                table: "RefundStock");

            migrationBuilder.RenameTable(
                name: "RefundStock",
                newName: "RefundStocks");

            migrationBuilder.RenameIndex(
                name: "IX_RefundStock_CartID",
                table: "RefundStocks",
                newName: "IX_RefundStocks_CartID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefundStocks",
                table: "RefundStocks",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundStocks_Carts_CartID",
                table: "RefundStocks",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RefundStocks_RefundStockID",
                table: "Sales",
                column: "RefundStockID",
                principalTable: "RefundStocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefundStocks_Carts_CartID",
                table: "RefundStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RefundStocks_RefundStockID",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefundStocks",
                table: "RefundStocks");

            migrationBuilder.RenameTable(
                name: "RefundStocks",
                newName: "RefundStock");

            migrationBuilder.RenameIndex(
                name: "IX_RefundStocks_CartID",
                table: "RefundStock",
                newName: "IX_RefundStock_CartID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefundStock",
                table: "RefundStock",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundStock_Carts_CartID",
                table: "RefundStock",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RefundStock_RefundStockID",
                table: "Sales",
                column: "RefundStockID",
                principalTable: "RefundStock",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
