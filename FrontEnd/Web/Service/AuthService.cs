using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly GateWay _gateWay;

        //public AuthService(IBaseService baseService, GateWay gateWay)
        //{
        //    _baseService = baseService;
        //    _gateWay = gateWay;
        //}
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequest obj)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequest loginRequestDto)
        {
            //return await _gateWay.SendAsync(new RequestDto()
            //{
            //    ApiType = SD.ApiType.POST,
            //    Data = loginRequestDto,
            //    Url = SD.AuthAPIBase + "/api/auth/login"
            //});

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequest registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
