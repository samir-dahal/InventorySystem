using InventorySystem.API.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public TheDbContext TheDbContext => Context as TheDbContext;
        public SaleRepository(TheDbContext context) : base(context) { }

        public async Task<IEnumerable<Sale>> GetAllWithProductAsync()
        {
            return await TheDbContext.Sales
                .Include(sale => sale.Purchase.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Sale> GetWithProductAsync(int id)
        {
            return await TheDbContext.Sales
                .Include(sale => sale.Purchase.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(sale => sale.Id == id);
        }
    }
}
