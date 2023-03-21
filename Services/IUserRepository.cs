using Todo_Assignment.API.Data.Entities;

namespace Todo_Assignment.API.Services
{
    public interface IUserRepository
    {
        void RegisterUser(string userName, byte[] passwordHash, byte[] passwordSalt);
        UserEntity? GetUserByUserName(string userName);
        Task<bool> SaveChangesAsync();
    }
}
