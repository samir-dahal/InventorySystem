using InventorySystem.API.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public TheDbContext TheDbContext => Context as TheDbContext;
        public CustomerRepository(TheDbContext context) : base(context) { }
    }
}
