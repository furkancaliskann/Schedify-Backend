using DataAccess.Pagination;
using Entities.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IRepositoryBase<T> where T : class, IEntity, new()
    {
        Task<PagedResponse<T>> GetAllAsync(string userId, PaginationQuery paginationQuery);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
