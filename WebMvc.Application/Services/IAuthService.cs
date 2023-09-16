using System.Threading.Tasks;
using WebMvc.Application.Models.User;
using WebMvc.Application.Services.Models;

namespace WebMvc.Application.Services;

public interface IAuthService
{
    Task Register(User user);
    Task<UserInfo> Login(User user);
}