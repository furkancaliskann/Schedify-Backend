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

        public async Task<PagedResponse<Todo>> GetAllAsync(string userId, PaginationQuery paginationQuery)
        {
            var todosQuery = _context.Todos.Where(x => x.CreatedUserId == userId).AsQueryable(); // UserId'ye göre filtreleme

            // Arama (Search)
            if (!string.IsNullOrEmpty(paginationQuery.SearchTerm))
            {
                var searchTerm = paginationQuery.SearchTerm.ToLower();

                todosQuery = todosQuery.Where(x =>
                    EF.Functions.Like(x.Title.ToLower(), $"%{searchTerm}%"));
            }

            // Sıralama (Sorting)
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

            // Toplam Sayı Hesaplama
            var totalCount = await todosQuery.CountAsync();

            // Verileri Çekme (Pagination)
            var items = await todosQuery
                .Include(t => t.CreatedUser) // İlişkili kullanıcıyı dahil etme
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();

            // Sonuçları Döndürme
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
            return await _context.Todos.Include(t => t.CreatedUser).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Todo entity)
        {
            _context.Todos.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
