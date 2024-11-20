using Business.Dtos;
using DataAccess.Pagination;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITodoService
    {
        Task<PagedResponse<Todo>> GetAllAsync(PaginationQuery paginationQuery);
        Task<Todo> GetByIdAsync(int id);
        Task AddAsync(CreateTodoDto entity);
        Task UpdateAsync(int id, UpdateTodoDto entity);
        Task DeleteAsync(int id);
    }
}
