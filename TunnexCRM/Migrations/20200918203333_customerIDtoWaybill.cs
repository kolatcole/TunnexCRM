using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class customerIDtoWaybill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "customerID",
                table: "Waybills",
                nullable: false,
                defaultValue: 0);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnedStocks_Carts_CartID",
                table: "ReturnedStocks");

            migrationBuilder.DropColumn(
                name: "customerID",
                table: "Waybills");

            migrationBuilder.AlterColumn<int>(
                name: "CartID",
                table: "ReturnedStocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnedStocks_Carts_CartID",
                table: "ReturnedStocks",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
