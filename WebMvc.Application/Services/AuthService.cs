using System.Threading.Tasks;
using WebMvc.Application.Models.User;

namespace WebMvc.Application.Services;

public class AuthService : IAuthService
{
    public Task<UserInfo> TryRegister(User user)
    {
        throw new System.NotImplementedException();
    }

    public Task<UserInfo> TryAuthenticate(User user)
    {
        throw new System.NotImplementedException();
    }
}