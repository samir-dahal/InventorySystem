using InventorySystem.API.DataAccess.Repositories;
using InventorySystem.API.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TheDbContext _context;
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
        public IPurchaseRepository PurchaseRepository { get; }
        public UnitOfWork(TheDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
            SupplierRepository = new SupplierRepository(_context);
            PurchaseRepository = new PurchaseRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
