using InventorySystem.API.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        Task<int> CompleteAsync();
    }
}
