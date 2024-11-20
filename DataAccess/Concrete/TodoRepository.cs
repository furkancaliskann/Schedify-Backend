using DataAccess.Abstract;
using DataAccess.Pagination;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class TodoRepository : ITodoRepository
    {
        private readonly SchedifyContext _context;

        public TodoRepository(SchedifyContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Todo entity)
        {
            await _context.Todos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PagedResponse<Todo>> GetAllAsync(PaginationQuery paginationQuery)
        {
            var todosQuery = _context.Todos.AsQueryable();

            if (!string.IsNullOrEmpty(paginationQuery.SearchTerm))
            {
                var searchTerm = paginationQuery.SearchTerm.ToLower();

                todosQuery = todosQuery.Where(x =>
                    EF.Functions.Like(x.Title.ToLower(), $"%{searchTerm}%"));
            }

            if (!string.IsNullOrEmpty(paginationQuery.SortBy))
            {
                var normalizedSortBy = char.ToUpper(paginationQuery.SortBy[0]) + paginationQuery.SortBy.Substring(1).ToLower();
                todosQuery = paginationQuery.SortDirection?.ToLower() switch
                {
                    "asc" => todosQuery.OrderBy(x => EF.Property<object>(x, normalizedSortBy)),
                    "desc" => todosQuery.OrderByDescending(x => EF.Property<object>(x, normalizedSortBy)),
                    _ => todosQuery
                };
            }

            var totalCount = await todosQuery.CountAsync();
            var items = await todosQuery
            .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();

            return new PagedResponse<Todo>
            {
                Data = items,
                TotalCount = totalCount,
                PageNumber = paginationQuery.PageNumber,
                PageSize = paginationQuery.PageSize
            };
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task UpdateAsync(Todo entity)
        {
            _context.Todos.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
