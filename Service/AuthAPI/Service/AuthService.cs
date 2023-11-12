using AuthAPI.Models;
using AuthAPI.Service.IService;
using Azure;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _db;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == loginRequest.Username);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (isValid)
            {
                LoginResponse response = new LoginResponse()
                {
                    User = user,
                    Token = "asd"
                };
                return response;
            }
            return new LoginResponse() { User = null, Token = "" };
        }

        public async Task<string> Register(RegistrationRequest registrationRequest)
        {
            User user = new()
            {
                UserName = registrationRequest.Email,
                Email = registrationRequest.Email,
                NormalizedEmail = registrationRequest.Email.ToUpper(),
                Name = registrationRequest.Name,
                PhoneNumber = registrationRequest.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);

            if (result.Succeeded)
            {
                return "Success";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
    }
}
