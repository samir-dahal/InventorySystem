using Microsoft.EntityFrameworkCore.Migrations;

namespace InventorySystem.API.Migrations
{
    public partial class update_datamodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Products_ProductId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Sales",
                newName: "PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                newName: "IX_Sales_PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "Sales",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_PurchaseId",
                table: "Sales",
                newName: "IX_Sales_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Products_ProductId",
                table: "Sales",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
