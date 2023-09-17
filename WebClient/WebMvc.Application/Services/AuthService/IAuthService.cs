using System;
using System.Threading.Tasks;
using WebMvc.Application.Models;

namespace WebMvc.Application.Services.AuthService;

public interface IAuthService
{
    Task RegisterAsync(User user);
    Task<UserInfo> LoginAsync(User user);

    Task<bool> ContainsAsync(string login, Guid token);
}