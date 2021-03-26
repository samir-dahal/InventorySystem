using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories.Interfaces
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<Purchase> GetWithProductAsync(int id);
        Task<IEnumerable<Purchase>> GetAllWithProductAsync();
    }
}
