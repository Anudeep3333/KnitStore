using Ecommerce.Models;

namespace Ecommerce.Services;

public interface IUserService
{
    Task<User> RegisterUser(RegisterRequest registerRequest);
    Task<User> LoginUser(LoginRequest loginRequest);
    Task<User> GetUser(int userId);
}