using InventorySystem.API.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public TheDbContext TheDbContext => Context as TheDbContext;
        public SupplierRepository(TheDbContext context) : base(context) { }
    }
}
