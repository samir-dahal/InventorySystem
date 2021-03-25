using InventorySystem.API.DataAccess.Repositories.Interfaces;
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
    }
}
