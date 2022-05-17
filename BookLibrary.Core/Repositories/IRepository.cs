using BookLibrary.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id, string[] includeProperties = null);
        Task<IEnumerable<T>> BrowseAllAsync(
            Expression<Func<T, bool>> filter = null,
            string[] includeProperties = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> IsExist<Tcheck>(int id) where Tcheck : Entity;
        Task SaveAsync();
    }
}
