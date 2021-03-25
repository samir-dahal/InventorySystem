using InventorySystem.API.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public TheDbContext TheDbContext => Context as TheDbContext;
        public CategoryRepository(TheDbContext context) : base(context) { }
    }
}
