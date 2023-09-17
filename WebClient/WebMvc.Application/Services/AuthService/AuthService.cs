using System;
using System.Threading.Tasks;
using WebMvc.Application.Models;

namespace WebMvc.Application.Services.AuthService;

public class AuthService : IAuthService
{
    public Task RegisterAsync(User user)
    {
        throw new System.NotImplementedException();
    }

    public Task<UserInfo> LoginAsync(User user)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> ContainsAsync(string login, Guid token)
    {
        throw new NotImplementedException();
    }
}