using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class returnedStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RefundStocks_RefundStockID",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "RefundStocks");

            migrationBuilder.DropIndex(
                name: "IX_Sales_RefundStockID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "RefundStockID",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "ReturnedStockID",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReturnedStocks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CartID = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedStocks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReturnedStocks_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ReturnedStockID",
                table: "Sales",
                column: "ReturnedStockID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedStocks_CartID",
                table: "ReturnedStocks",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ReturnedStocks_ReturnedStockID",
                table: "Sales",
                column: "ReturnedStockID",
                principalTable: "ReturnedStocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ReturnedStocks_ReturnedStockID",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "ReturnedStocks");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ReturnedStockID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ReturnedStockID",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "RefundStockID",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefundStocks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundStocks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RefundStocks_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_RefundStockID",
                table: "Sales",
                column: "RefundStockID");

            migrationBuilder.CreateIndex(
                name: "IX_RefundStocks_CartID",
                table: "RefundStocks",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RefundStocks_RefundStockID",
                table: "Sales",
                column: "RefundStockID",
                principalTable: "RefundStocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
