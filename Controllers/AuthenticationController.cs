﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo_Assignment.API.Data.DbContexts;
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
        public async Task<IActionResult> Register(UserModel request)
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
        public IActionResult Authenticate(UserModel request)
        {
            try
            {
                // Step 1: Validate Login/Authenticate Request
                var user = _userRepository.ValidateUserCredentials(request);
                if (user == null)
                {
                    return BadRequest("Invalid Username or Password.");
                }

                // Step 2: Create Tokens
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim("userName", user.UserName.ToString())
                };

                _tokenService.GenerateTokens(user, claims, out string authToken, out string refreshToken);

                return Ok(new AuthenticatedResponseModel
                {
                    AuthToken = authToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: Authentication");
                return BadRequest("Unable to authenticate user.");
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken(TokenApiModel tokenApiModel)
        {
            try
            {
                if (tokenApiModel is null)
                {
                    return BadRequest("Invalid client request.");
                }
                else
                {
                    string retrievedAuthToken = tokenApiModel.AuthToken!;
                    string retrievedRefreshToken = tokenApiModel.RefreshToken!;

                    var principal = _tokenService.GetPrincipalFromExpiredToken(retrievedAuthToken);
                    var username = principal.Identity!.Name;

                    var user = _context.Users.SingleOrDefault(u => u.UserName == username);
                    if (user is null || user.RefreshToken != retrievedRefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    {
                        return Unauthorized("Invalid or Expired Token/Session.");
                    }

                    _tokenService.GenerateTokens(user, principal.Claims, out string authToken, out string refreshToken);

                    return Ok(new AuthenticatedResponseModel
                    {
                        AuthToken = authToken,
                        RefreshToken = refreshToken
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: Refresh-Token");
                return BadRequest("Invalid client request.");
            }
        }

    }
}
