using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo_Assignment.API.Data.DbContexts;
using Todo_Assignment.API.Data.Entities;
using Todo_Assignment.API.Models;
using Todo_Assignment.API.Services;

namespace Todo_Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TodoDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<TaskController> _logger;

        public AuthenticationController(TodoDbContext context, IUserRepository userRepository, ITokenService tokenService, ILogger<TaskController> logger)
        {
            _context = context;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserEntity>> Register(UserModel request)
        {
            try
            {
                bool checkUserNameAvailable = _userRepository.GetUserByUserName(request.UserName) != null;
                if (checkUserNameAvailable)
                {
                    return UnprocessableEntity("UserName is taken.");
                }

                _userRepository.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

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
                var user = _userRepository.ValidateUserCredentials(request);
                if (user == null)
                {
                    return BadRequest("Invalid UserName or Password.");
                }

                // Step 2: create token
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim("userName", user.UserName.ToString())
                };

                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(2);
                _context.SaveChanges();

                return Ok(new AuthenticatedResponseModel
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: Authentication");
                return BadRequest("Unable to authenticate user.");
            }
        }
    }
}
