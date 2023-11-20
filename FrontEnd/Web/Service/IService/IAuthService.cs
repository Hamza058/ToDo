using Web.Models;

namespace Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequest obj);
        Task<ResponseDto?> LoginAsync(LoginRequest loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequest registrationRequestDto);
    }
}
