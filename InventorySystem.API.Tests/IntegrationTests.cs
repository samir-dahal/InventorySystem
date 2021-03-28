using InventorySystem.API.DataAccess;
using InventorySystem.Contracts.v1._0;
using InventorySystem.Contracts.v1._0.Requests.Category;
using InventorySystem.Contracts.v1._0.Requests.Customer;
using InventorySystem.Contracts.v1._0.Requests.Product;
using InventorySystem.Contracts.v1._0.Requests.Purchase;
using InventorySystem.Contracts.v1._0.Requests.Sale;
using InventorySystem.Contracts.v1._0.Requests.Supplier;
using InventorySystem.Contracts.v1._0.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.API.Tests
{
    public class IntegrationTests
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }
        protected async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "/categories", request);
            return await response.Content.ReadAsAsync<CategoryResponse>();
        }
        protected async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "/products", request);
            return await response.Content.ReadAsAsync<ProductResponse>();
        }
        protected async Task<CustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "/customers", request);
            return await response.Content.ReadAsAsync<CustomerResponse>();
        }
        protected async Task<SaleResponse> CreateSaleAsync(IEnumerable<CreateSaleRequest> requests)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "/sales", requests);
            return await response.Content.ReadAsAsync<SaleResponse>();
        }
        protected async Task<PurchaseResponse> CreatePurchaseAsync(CreatePurchaseRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "/purchases", request);
            return await response.Content.ReadAsAsync<PurchaseResponse>();
        }
        protected async Task<SupplierResponse> CreateSupplierAsync(CreateSupplierRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "/suppliers", request);
            return await response.Content.ReadAsAsync<SupplierResponse>();
        }
        //get
        protected CreateCategoryRequest GetCategoryRequest()
        {
            return new CreateCategoryRequest
            {
                Name = "Test Category",
            };
        }
        protected CreateCustomerRequest GetCustomerRequest()
        {
            return new CreateCustomerRequest
            {
                Name = "Test Customer",
                Email = "customer@email.com",
                Phone = "9814567894",
            };
        }
        protected CreateSupplierRequest GetSupplierRequest()
        {
            return new CreateSupplierRequest
            {
                Name = "Test Supplier",
                Email = "supplier@email.com",
                Phone = "9814567894",
            };
        }
    }
}
