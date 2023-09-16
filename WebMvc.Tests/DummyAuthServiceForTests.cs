using WebMvc.Application.Models;
using WebMvc.Application.Services;

namespace WebMvc.Tests;

public class DummyAuthServiceForTests : DummyAuthService
{
    public Dictionary<User, UserInfo> GetDb => GetDataBase();
}