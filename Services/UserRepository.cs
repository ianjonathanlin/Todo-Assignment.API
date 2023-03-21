using Todo_Assignment.API.Data.DbContexts;
using Todo_Assignment.API.Data.Entities;

namespace Todo_Assignment.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoDbContext _context;

        public UserRepository(TodoDbContext todoDbContext)
        {
            _context = todoDbContext;
        }

        public void RegisterUser(string userName, byte[] passwordHash, byte[] passwordSalt)
        {
            UserEntity user = new UserEntity
            {
                UserName = userName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(user);
        }

        public UserEntity? GetUserByUserName(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
