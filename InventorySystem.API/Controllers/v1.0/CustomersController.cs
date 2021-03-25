using InventorySystem.API.DataAccess;
using InventorySystem.Contracts.v1._0.Requests.Customer;
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
    public class CustomersController : BaseController
    {
        public CustomersController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
            };
            await UnitOfWork.CustomerRepository.AddAsync(customer);
            await UnitOfWork.CompleteAsync();
            return Ok(new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(id);
            if (customer is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No customer found with id {id}" }
                });
            }
            return Ok(new CustomerResponse
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                Phone = customer.Phone,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await UnitOfWork.CustomerRepository.GetAllAsync();
            return Ok(new Response<CustomerResponse>
            {
                Data = customers.Select(customer => new CustomerResponse
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone,
                })
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(id);
            if (customer is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No customer found with id {id}" }
                });
            }
            UnitOfWork.CustomerRepository.Remove(customer);
            await UnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCustomerRequest request)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(id);
            if (customer is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new string[] { $"No customer found with id {id}" }
                });
            }
            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            await UnitOfWork.CompleteAsync();
            return Ok(new CustomerResponse
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                Phone = customer.Phone,
            });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAllBySearchQueryAsync(string query)
        {
            var customers = await UnitOfWork.CustomerRepository.FindAsync(customer => customer.Name.Contains(query));
            return Ok(new Response<CustomerResponse>
            {
                Data = customers.Select(customer => new CustomerResponse
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone,
                })
            });
        }
    }
}
