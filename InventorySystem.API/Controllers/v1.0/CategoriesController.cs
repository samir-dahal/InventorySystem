using InventorySystem.API.DataAccess;
using InventorySystem.Contracts.v1._0.Requests.Category;
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
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
            };
            await UnitOfWork.CategoryRepository.AddAsync(category);
            await UnitOfWork.CompleteAsync();
            return Ok(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var category = await UnitOfWork.CategoryRepository.GetAsync(id);
            if (category is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No category found with id {id}" }
                });
            }
            return Ok(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await UnitOfWork.CategoryRepository.GetAllAsync();
            return Ok(new Response<CategoryResponse>
            {
                Data = categories.Select(category => new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                })
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var category = await UnitOfWork.CategoryRepository.GetAsync(id);
            if (category is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No category found with id {id}" }
                });
            }
            UnitOfWork.CategoryRepository.Remove(category);
            await UnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCategoryRequest request)
        {
            var category = await UnitOfWork.CategoryRepository.GetAsync(id);
            if (category is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No category found with id {id}" }
                });
            }
            category.Name = request.Name;
            await UnitOfWork.CompleteAsync();
            return Ok(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
            });
        }
    }
}
