using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories.Interfaces
{
    public interface ISaleRepository : IRepository<Sale>
    {
        Task<IEnumerable<Sale>> GetAllWithProductAsync();
        Task<Sale> GetWithProductAsync(int id);
    }
}
