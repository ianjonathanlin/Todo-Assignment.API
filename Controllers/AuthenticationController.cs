using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Todo_Assignment.API.Data.Entities;
using Todo_Assignment.API.Models;
using Todo_Assignment.API.Services;

namespace Todo_Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TaskController> _logger;

        public AuthenticationController(IUserRepository userRepository, IConfiguration configuration, ILogger<TaskController> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserEntity>> Register(UserModel request)
        {
            try
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                _userRepository.RegisterUser(request.UserName, passwordHash, passwordSalt);
                await _userRepository.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: Register");
                return BadRequest("Unable to register user.");
            }
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(UserModel request)
        {
            try
            {
                // Step 1: validate login request
                var user = ValidateUserCredentials(request);
                if (user == null)
                {
                    return BadRequest("Invalid username or password");
                }

                // Step 2: create a token
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // claims
                var claimsForToken = new List<Claim>
                {
                    new Claim("sub", user.UserId.ToString()),
                    new Claim("userName", user.UserName.ToString())
                };

                // create jwt Security Token
                var jwtSecurityToken = new JwtSecurityToken(
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials);

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return Ok(tokenToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: Authentication");
                return BadRequest("Unable to authenticate user.");
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
        }

        private UserEntity? ValidateUserCredentials(UserModel request)
        {
            // check username & password against what's stored in db to check if credentials are valid
            UserEntity? user = _userRepository.GetUserByUserName(request.UserName);
            if (user != null && VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
