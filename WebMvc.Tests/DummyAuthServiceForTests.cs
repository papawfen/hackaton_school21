using WebMvc.Application.Models.User;
using WebMvc.Application.Services;
using WebMvc.Application.Services.Models;

namespace WebMvc.Tests;

public class DummyAuthServiceForTests : DummyAuthService
{
    public Dictionary<User, UserInfo> GetDb => GetDataBase();
}