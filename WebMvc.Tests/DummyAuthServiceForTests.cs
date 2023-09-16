using WebMvc.Application.Models;
using WebMvc.Application.Services;
using WebMvc.Application.Services.AuthService;

namespace WebMvc.Tests;

public class DummyAuthServiceForTests : DummyAuthService
{
    public Dictionary<User, UserInfo> GetDb => GetDataBase();
}