using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class RefundStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefundStockID",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefundStock",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CartID = table.Column<int>(nullable: true),
                    InvoiceNo = table.Column<string>(nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundStock", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RefundStock_Carts_CartID",
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
                name: "IX_RefundStock_CartID",
                table: "RefundStock",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RefundStock_RefundStockID",
                table: "Sales",
                column: "RefundStockID",
                principalTable: "RefundStock",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RefundStock_RefundStockID",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "RefundStock");

            migrationBuilder.DropIndex(
                name: "IX_Sales_RefundStockID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "RefundStockID",
                table: "Sales");
        }
    }
}
