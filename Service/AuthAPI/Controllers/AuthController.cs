using AuthAPI.Models;
using AuthAPI.Service.IService;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;

        public AuthController(SignInManager<User> signInManager, IAuthService authService)
        {
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = await _authService.Login(loginRequest);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registration)
        {
            var user = await _authService.Register(registration);
            return Ok(user);
        }
    }
}
