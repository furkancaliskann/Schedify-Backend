using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
