using Microsoft.EntityFrameworkCore.Migrations;

namespace InventorySystem.API.Migrations
{
    public partial class restric_purchase_deletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
