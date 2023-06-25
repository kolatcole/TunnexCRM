using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class RemovedCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefundStocks_Carts_CartID",
                table: "RefundStocks");

            migrationBuilder.DropIndex(
                name: "IX_RefundStocks_CartID",
                table: "RefundStocks");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "RefundStocks");

            migrationBuilder.AddColumn<int>(
                name: "RefundStockID",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_RefundStockID",
                table: "Items",
                column: "RefundStockID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_RefundStocks_RefundStockID",
                table: "Items",
                column: "RefundStockID",
                principalTable: "RefundStocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_RefundStocks_RefundStockID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_RefundStockID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RefundStockID",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "RefundStocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundStocks_CartID",
                table: "RefundStocks",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundStocks_Carts_CartID",
                table: "RefundStocks",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
