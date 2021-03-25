using InventorySystem.API.DataAccess;
using InventorySystem.Contracts.v1._0.Requests.Sale;
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
    public class SalesController : BaseController
    {
        public SalesController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSaleRequest request)
        {
            var sale = new Sale
            {
                CustomerId = request.CustomerId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.TotalPrice,
            };
            await UnitOfWork.SaleRepository.AddAsync(sale);
            await UnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var sale = await UnitOfWork.SaleRepository.GetAsync(id);
            if (sale is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No sale found with id {id}" }
                });
            }
            return Ok(new SaleResponse
            {
                Id = sale.Id,
                CustomerId = sale.CustomerId,
                ProductId = sale.ProductId,
                Quantity = sale.Quantity,
                TotalPrice = sale.TotalPrice,
                UnitPrice = sale.UnitPrice,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var sales = await UnitOfWork.SaleRepository.GetAllAsync();
            return Ok(new Response<SaleResponse>
            {
                Data = sales.Select(sale => new SaleResponse
                {
                    Id = sale.Id,
                    CustomerId = sale.CustomerId,
                    ProductId = sale.ProductId,
                    Quantity = sale.Quantity,
                    TotalPrice = sale.TotalPrice,
                    UnitPrice = sale.UnitPrice,
                })
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var sale = await UnitOfWork.SaleRepository.GetAsync(id);
            if (sale is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No sale found with id {id}" }
                });
            }
            UnitOfWork.SaleRepository.Remove(sale);
            await UnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateSaleRequest request)
        {
            var sale = await UnitOfWork.SaleRepository.GetAsync(id);
            if (sale is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No sale found with id {id}" }
                });
            }
            sale.CustomerId = request.CustomerId;
            sale.ProductId = request.ProductId;
            sale.UnitPrice = request.UnitPrice;
            sale.Quantity = request.Quantity;
            sale.TotalPrice = request.TotalPrice;
            await UnitOfWork.CompleteAsync();
            return Ok(new SaleResponse
            {
                Id = sale.Id,
                CustomerId = sale.CustomerId,
                ProductId = sale.ProductId,
                Quantity = sale.Quantity,
                TotalPrice = sale.TotalPrice,
                UnitPrice = sale.UnitPrice,
            });
        }
    }
}
