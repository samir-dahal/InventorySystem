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
        public PurchaseRepository(TheDbContext context) : base(context) { }

        public async Task<Purchase> GetWithProductAsync(int id)
        {
            return await TheDbContext.Purchases
                .Include(purchase => purchase.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(purchase => purchase.Id == id);
        }

        public async Task<IEnumerable<Purchase>> GetAllWithProductAsync()
        {
            return await TheDbContext.Purchases
                .Include(purchase => purchase.Product)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
