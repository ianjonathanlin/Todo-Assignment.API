using Todo_Assignment.API.Data.Entities;
using Todo_Assignment.API.Models;

namespace Todo_Assignment.API.Services
{
    public interface IUserRepository
    {
        void RegisterUser(string userName, byte[] passwordHash, byte[] passwordSalt);
        UserEntity? GetUserByUserName(string userName);
        UserEntity? ValidateUserCredentials(UserModel request);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<bool> SaveChangesAsync();
    }
}
