using WebMvc.Application.Models.User;
using WebMvc.Application.Services;

namespace WebMvc.Tests;

public class DummyAuthServiceForTests : DummyAuthService
{
    public Dictionary<User, UserInfo> GetDb => GetDataBase();
}