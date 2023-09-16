using System.Threading.Tasks;
using WebMvc.Application.Models.User;

namespace WebMvc.Application.Services;

public interface IAuthService
{
    Task<UserInfo> TryRegister(User user);
    Task<UserInfo> TryAuthenticate(User user);
}