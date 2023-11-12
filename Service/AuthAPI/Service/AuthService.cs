using AuthAPI.Models;
using AuthAPI.Service.IService;
using Azure;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == loginRequest.Username);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            if (isValid)
            {
                LoginResponse response = new LoginResponse()
                {
                    User = user,
                    Token = token
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
