using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly SchedifyContext _context;

        public UserRepository(SchedifyContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            var check = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (check != null)
            {
                Console.WriteLine("Kullanıcı eklenmedi!");
                return;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            Console.WriteLine("Kullanıcı başarıyla eklendi!");
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}
