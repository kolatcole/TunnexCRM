using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Presentation.Core.Migrations
{
    public partial class ist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleID",
                table: "AppUsers",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Roles_RoleID",
                table: "AppUsers",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Roles_RoleID",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Carts_CartID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CartID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_RoleID",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "AppUsers");
        }
    }
}
