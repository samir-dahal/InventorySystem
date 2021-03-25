using InventorySystem.API.DataAccess;
using InventorySystem.Contracts.v1._0.Requests.Supplier;
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
    public class SuppliersController : BaseController
    {
        public SuppliersController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSupplierRequest request)
        {
            var supplier = new Supplier
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
            };
            await UnitOfWork.SupplierRepository.AddAsync(supplier);
            await UnitOfWork.CompleteAsync();
            return Ok(new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var supplier = await UnitOfWork.SupplierRepository.GetAsync(id);
            if (supplier is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No supplier found with id {id}" }
                });
            }
            return Ok(new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var suppliers = await UnitOfWork.SupplierRepository.GetAllAsync();
            return Ok(new Response<SupplierResponse>
            {
                Data = suppliers.Select(supplier => new SupplierResponse
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
                })
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var supplier = await UnitOfWork.SupplierRepository.GetAsync(id);
            if (supplier is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No supplier found with id {id}" }
                });
            }
            UnitOfWork.SupplierRepository.Remove(supplier);
            await UnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateSupplierRequest request)
        {
            var supplier = await UnitOfWork.SupplierRepository.GetAsync(id);
            if (supplier is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No supplier found with id {id}" }
                });
            }
            supplier.Name = request.Name;
            supplier.Email = request.Email;
            supplier.Phone = request.Phone;
            await UnitOfWork.CompleteAsync();
            return Ok(new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
            });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAllBySearchQueryAsync(string query)
        {
            var suppliers = await UnitOfWork.SupplierRepository.FindAsync(supplier => supplier.Name.Contains(query));
            return Ok(new Response<SupplierResponse>
            {
                Data = suppliers.Select(supplier => new SupplierResponse
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
                })
            });
        }
    }
}
