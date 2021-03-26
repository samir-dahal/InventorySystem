using Microsoft.EntityFrameworkCore.Migrations;

namespace InventorySystem.API.Migrations
{
    public partial class added_delete_restrictions_to_data_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                table: "Purchases",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Purchases_PurchaseId",
                table: "Sales");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                table: "Purchases",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
