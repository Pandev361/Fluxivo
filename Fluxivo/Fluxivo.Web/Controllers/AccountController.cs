using Fluxivo.Infrastructure.Services;
using Fluxivo.Shared.Auth;
using Fluxivo.Shared.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Fluxivo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserService _userService;

        public AccountController(JwtService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login.LoginRequest request)
        {
            var user = await _userService.ValidateUserAsync(request.UserName, request.Password);
            if (user == null)
                return Unauthorized("Benutzername oder Passwort falsch.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                Token = token,
                UserName = user.UserName
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register.RegisterRequest request)
        {
            // Attempt to register the user
            var user = await _userService.RegisterUserAsync(request.UserName, request.Email,request.Password);
            if (user == null)
                return BadRequest("Benutzername existiert bereits.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new AuthResponse
            {
                Token = token,
                UserName = user.UserName
            });
        }
    }
}