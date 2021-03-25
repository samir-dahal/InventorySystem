using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //get
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        //add
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        //remove
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
