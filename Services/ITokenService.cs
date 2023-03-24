using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Todo_Assignment.API.Data.Entities;

namespace Todo_Assignment.API.Services
{
    public interface ITokenService
    {
        void GenerateTokens(UserEntity user, IEnumerable<Claim> claims, out string authToken, out string refreshToken);
        string GenerateAuthToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
