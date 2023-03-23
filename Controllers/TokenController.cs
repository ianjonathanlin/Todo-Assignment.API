using Microsoft.AspNetCore.Mvc;
using Todo_Assignment.API.Data.DbContexts;
using Todo_Assignment.API.Models;
using Todo_Assignment.API.Services;

namespace Todo_Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TodoDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<TokenController> _logger;

        public TokenController(TodoDbContext context, ITokenService tokenService, ILogger<TokenController> logger)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            try
            {
                if (tokenApiModel is null)
                {
                    return BadRequest("Invalid client request. 1");
                }
                else
                {
                    string accessToken = tokenApiModel.AccessToken;
                    string refreshToken = tokenApiModel.RefreshToken;

                    var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
                    var username = principal.Identity.Name;

                    var user = _context.Users.SingleOrDefault(u => u.UserName == username);
                    //if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    //{
                    //    return BadRequest("Invalid client request.");
                    //}

                    if (user is null)
                    {
                        return BadRequest("Invalid client request.2");
                    } else if (user.RefreshToken != refreshToken)
                    {
                        return BadRequest("Invalid client request.3");
                    } else if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    {
                        return BadRequest("Invalid client request.4" + user.RefreshTokenExpiryTime);
                    }

                    var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
                    var newRefreshToken = _tokenService.GenerateRefreshToken();

                    user.RefreshToken = newRefreshToken;
                    _context.SaveChanges();

                    return Ok(new AuthenticatedResponseModel
                    {
                        Token = newAccessToken,
                        RefreshToken = newRefreshToken
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Action: Refresh-Token");
                return BadRequest("Invalid client request. Catch");
            }
        }
    }
}
