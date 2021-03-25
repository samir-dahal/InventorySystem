using InventorySystem.API.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public TheDbContext TheDbContext => Context as TheDbContext;
        public ProductRepository(TheDbContext context) : base(context) { }
    }
}
