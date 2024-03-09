using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<List<UserDto>> GetUsers();
        Task<string> UpsertSecurityQuestion(SecurityQuestions securityQuestionRequest);

    }
}
