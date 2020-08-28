using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class purchaseProdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExchangeCurrency",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NairaEquivalent",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountForeign",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountNaira",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PurchaseProducts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cartID = table.Column<int>(nullable: false),
                    productID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProducts", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "ExchangeCurrency",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "NairaEquivalent",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "TotalAmountForeign",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "TotalAmountNaira",
                table: "Purchases");
        }
    }
}
