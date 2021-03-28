using FluentAssertions;
using InventorySystem.Contracts.v1._0;
using InventorySystem.Contracts.v1._0.Requests.Category;
using InventorySystem.Contracts.v1._0.Requests.Customer;
using InventorySystem.Contracts.v1._0.Requests.Product;
using InventorySystem.Contracts.v1._0.Requests.Purchase;
using InventorySystem.Contracts.v1._0.Requests.Sale;
using InventorySystem.Contracts.v1._0.Requests.Supplier;
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
    public class CustomersControllerTests : IntegrationTests
    {
        [Fact]
        public async Task DeleteAsync_ShouldNotDeleteCustomer_WhenReferencedBySale()
        {
            //Arrange
            var customer = await CreateCustomerAsync(GetCustomerRequest());
            var category = await CreateCategoryAsync(GetCategoryRequest());
            var product = await CreateProductAsync(new CreateProductRequest
            {
                CategoryId = category.Id,
                Code = "12xx",
                Name = "Test Proudct",
            });
            var supplier = await CreateSupplierAsync(GetSupplierRequest());
            var purchase = await CreatePurchaseAsync(new CreatePurchaseRequest
            {
                Quantity = 20,
                UnitPrice = 100,
                TotalPrice = 2000,
                SupplierId = supplier.Id,
                ProductId = product.Id,
            });
            var sale = await CreateSaleAsync(new List<CreateSaleRequest>
            {
                new CreateSaleRequest
                {
                    CustomerId = customer.Id,
                    PurchaseId = purchase.Id,
                    Quantity = 1,
                    UnitPrice = 100,
                    TotalPrice = 100,
                }
            });

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Base + $"/customers/{customer.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteCustomer_WhenNotReferencedBySale()
        {
            //Arrange
            var customer = await CreateCustomerAsync(GetCustomerRequest());

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Base + $"/customers/{customer.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
