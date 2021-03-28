using FluentAssertions;
using InventorySystem.Contracts.v1._0;
using InventorySystem.Contracts.v1._0.Requests.Category;
using InventorySystem.Contracts.v1._0.Requests.Product;
using InventorySystem.Contracts.v1._0.Requests.Purchase;
using InventorySystem.Contracts.v1._0.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace InventorySystem.API.Tests.Controllers.v1._0
{
    public class SuppliersControllerTests : IntegrationTests
    {
        [Fact]
        public async Task DeleteAsync_ShouldNotDeleteSupplier_WhenReferencedByPurchase()
        {
            //Arrange
            var supplier = await CreateSupplierAsync(GetSupplierRequest());
            var category = await CreateCategoryAsync(GetCategoryRequest());
            var product = await CreateProductAsync(new CreateProductRequest
            {
                CategoryId = category.Id,
                Name = "Test Product",
                Code = "2344",
            });
            await CreatePurchaseAsync(new CreatePurchaseRequest
            {
                ProductId = product.Id,
                SupplierId = supplier.Id,
                Quantity = 10,
                UnitPrice = 100,
                TotalPrice = 1000,
            });

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Base + $"/suppliers/{supplier.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteSupplier_WhenNotReferencedByPurchase()
        {
            //Arrange
            var supplier = await CreateSupplierAsync(GetSupplierRequest());

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Base + $"/suppliers/{supplier.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
