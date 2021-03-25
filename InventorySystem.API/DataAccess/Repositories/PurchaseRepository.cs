using InventorySystem.API.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public TheDbContext TheDbContext => Context as TheDbContext;
        public PurchaseRepository(TheDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Purchase>> GetAllWithProductAndSupplierAsync()
        {
            return await TheDbContext.Purchases
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
