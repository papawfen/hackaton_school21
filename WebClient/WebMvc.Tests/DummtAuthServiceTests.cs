using WebMvc.Application.Models;
using WebMvc.Application.Services;

namespace WebMvc.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TryRegister_RegisterTwoUsers()
    {
        var authService = new DummyAuthServiceForTests();
        var users = new List<User>
        {
            new("First", "First"),
            new("Second", "Second")
        };
        users.ForEach(user => authService.RegisterAsync(user).Wait());

        Assert.That(authService.GetDb.All(dbUser => users.Contains(dbUser.Key)));
        Assert.That(authService.GetDb.Count, Is.EqualTo(2));
    }

    [Test]
    public void TryRegister_TryRegisterSameUser()
    {
        var authService = new DummyAuthServiceForTests();
        authService.RegisterAsync(new User("First", "First")).Wait();

        Assert.That(() => authService.RegisterAsync(new User("First", "First")).Wait(), Throws.Exception);
    }

    [Test]
    public void TryLogin_RegisterAndLogin_Success()
    {
        var authService = new DummyAuthServiceForTests();
        var user = new User("First", "First");
        authService.RegisterAsync(user).Wait();

        Assert.That(() => authService.LoginAsync(user), Throws.Nothing);
        Assert.That(authService.GetDb.ContainsKey(user));
    }
}