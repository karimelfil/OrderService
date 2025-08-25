using OrderService.Domain.Model;
using OrderService.Domain.Model.Auth;

namespace OrderService.Business.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Authenticate(LoginRequest request);
        Task<bool> ValidateToken(string token);
        string GenerateJwtToken(string username, string role);

        Task<RegisterResponse> Register(RegisterRequest request);
    }
}
