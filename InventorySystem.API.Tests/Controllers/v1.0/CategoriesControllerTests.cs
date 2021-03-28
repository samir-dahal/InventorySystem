using FluentAssertions;
using InventorySystem.Contracts.v1._0;
using InventorySystem.Contracts.v1._0.Requests.Category;
using InventorySystem.Contracts.v1._0.Requests.Product;
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
    public class CategoriesControllerTests : IntegrationTests
    {
        [Fact]
        public async Task DeleteAsync_ShouldNotDeleteCategory_WhenRefrenecedByProduct()
        {
            //Arrange
            var category = await CreateCategoryAsync(GetCategoryRequest());
            var product = await CreateProductAsync(new CreateProductRequest
            {
                CategoryId = category.Id,
                Name = "Test Product",
                Code = "3432",
            });

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Base + $"/categories/{category.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteCategory_WhenNotReferencedByProduct()
        {
            //Arrange
            var category = await CreateCategoryAsync(GetCategoryRequest());

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Base + $"/categories/{category.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
