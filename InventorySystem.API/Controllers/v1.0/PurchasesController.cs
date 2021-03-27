using InventorySystem.API.DataAccess;
using InventorySystem.Contracts.v1._0.Requests.Purchase;
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
    public class PurchasesController : BaseController
    {
        public PurchasesController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePurchaseRequest request)
        {
            var purchase = new Purchase
            {
                ProductId = request.ProductId,
                SupplierId = request.SupplierId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.TotalPrice,
            };
            await UnitOfWork.PurchaseRepository.AddAsync(purchase);
            await UnitOfWork.CompleteAsync();
            return Ok(new PurchaseResponse
            {
                Id = purchase.Id,
                ProductId = purchase.ProductId,
                SupplierId = purchase.SupplierId,
                Quantity = purchase.Quantity,
                UnitPrice = purchase.UnitPrice,
                TotalPrice = purchase.TotalPrice,
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var purchase = await UnitOfWork.PurchaseRepository.GetWithProductAsync(id);
            if (purchase is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No purchase found with id {id}" }
                });
            }
            return Ok(new PurchaseResponse
            {
                Id = purchase.Id,
                Product = purchase.Product.Name,
                SupplierId = purchase.SupplierId,
                ProductId = purchase.ProductId,
                Quantity = purchase.Quantity,
                UnitPrice = purchase.UnitPrice,
                TotalPrice = purchase.TotalPrice,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var purchases = await UnitOfWork.PurchaseRepository.GetAllWithProductAsync();
            return Ok(new Response<PurchaseResponse>
            {
                Data = purchases.Select(purchase => new PurchaseResponse
                {
                    Id = purchase.Id,
                    SupplierId = purchase.SupplierId,
                    Quantity = purchase.Quantity,
                    ProductId = purchase.ProductId,
                    UnitPrice = purchase.UnitPrice,
                    Product = purchase.Product.Name,
                    TotalPrice = purchase.TotalPrice,
                })
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var purchase = await UnitOfWork.PurchaseRepository.GetAsync(id);
            if (purchase is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No purchase found with id {id}" }
                });
            }
            UnitOfWork.PurchaseRepository.Remove(purchase);
            await UnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePurchaseRequest request)
        {
            var purchase = await UnitOfWork.PurchaseRepository.GetAsync(id);
            if (purchase is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No purchase found with id {id}" }
                });
            }
            purchase.SupplierId = request.SupplierId;
            purchase.Quantity = request.Quantity;
            purchase.ProductId = request.ProductId;
            purchase.UnitPrice = request.UnitPrice;
            purchase.TotalPrice = request.TotalPrice;
            await UnitOfWork.CompleteAsync();
            return Ok(new PurchaseResponse
            {
                Id = purchase.Id,
                SupplierId = purchase.SupplierId,
                ProductId = purchase.ProductId,
                UnitPrice = purchase.UnitPrice,
                TotalPrice = purchase.TotalPrice,
                Quantity = purchase.Quantity,
            });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAllBySearchQueryAsync(string query)
        {
            var purchases = await UnitOfWork.PurchaseRepository.GetAllWithProductAsync();
            return Ok(new Response<PurchaseResponse>
            {
                Data = purchases.Where(purchase => purchase.Product.Name.Contains(query)).Select(purchase => new PurchaseResponse
                {
                    Id = purchase.Id,
                    SupplierId = purchase.SupplierId,
                    Product = purchase.Product.Name,
                    ProductId = purchase.ProductId,
                    UnitPrice = purchase.UnitPrice,
                    TotalPrice = purchase.TotalPrice,
                    Quantity = purchase.Quantity,
                })
            });
        }
    }
}
