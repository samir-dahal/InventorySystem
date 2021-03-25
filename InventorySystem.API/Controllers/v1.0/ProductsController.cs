using InventorySystem.API.DataAccess;
using InventorySystem.Contracts.v1._0.Requests.Product;
using InventorySystem.Contracts.v1._0.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.Controllers.v1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Code = request.Code,
                CategoryId = request.CategoryId,
            };
            await UnitOfWork.ProductRepository.AddAsync(product);
            await UnitOfWork.CompleteAsync();
            return Ok(new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                CategoryId = product.CategoryId,
            });
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await UnitOfWork.ProductRepository.GetAsync(id);
            if (product is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No product found with id {id}" }
                });
            }
            return Ok(new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                CategoryId = product.CategoryId,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await UnitOfWork.ProductRepository.GetAllAsync();
            return Ok(new Response<ProductResponse>
            {
                Data = products.Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Code = product.Code,
                    CategoryId = product.CategoryId,
                })
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await UnitOfWork.ProductRepository.GetAsync(id);
            if(product is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No product found with id {id}" }
                });
            }
            UnitOfWork.ProductRepository.Remove(product);
            await UnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateProductRequest request)
        {
            var product = await UnitOfWork.ProductRepository.GetAsync(id);
            if (product is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No product found with id {id}" }
                });
            }
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.Code = request.Code;
            await UnitOfWork.CompleteAsync();
            return Ok(new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                CategoryId = product.CategoryId,
            });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAllBySearchQueryAsync(string query)
        {
            var products = await UnitOfWork.ProductRepository.FindAsync(product => product.Name.Contains(query));
            return Ok(new Response<ProductResponse>
            {
                Data = products.Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Code = product.Code,
                    CategoryId = product.CategoryId,
                })
            });
        }
    }
}
