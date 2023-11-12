using AuthAPI.Models;

namespace AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequest registrationRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
