using System.Threading.Tasks;
using WebMvc.Application.Models.User;

namespace WebMvc.Application.Services;

public class AuthService : IAuthService
{
    public Task Register(User user)
    {
        throw new System.NotImplementedException();
    }

    public Task<UserInfo> Login(User user)
    {
        throw new System.NotImplementedException();
    }
}