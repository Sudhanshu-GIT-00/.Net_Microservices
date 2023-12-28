using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string>roles);

    }
}
