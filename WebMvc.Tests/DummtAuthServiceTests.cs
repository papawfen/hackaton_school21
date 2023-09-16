using WebMvc.Application.Models.User;
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
        users.ForEach(user => authService.Register(user).Wait());

        Assert.That(authService.GetDb.All(dbUser => users.Contains(dbUser.Key)));
        Assert.That(authService.GetDb.Count, Is.EqualTo(2));
    }

    [Test]
    public void TryRegister_TryRegisterSameUser()
    {
        var authService = new DummyAuthServiceForTests();
        authService.Register(new User("First", "First")).Wait();

        Assert.That(() => authService.Register(new User("First", "First")).Wait(), Throws.Exception);
    }

    [Test]
    public void TryLogin_RegisterAndLogin_Success()
    {
        var authService = new DummyAuthServiceForTests();
        var user = new User("First", "First");
        authService.Register(user).Wait();

        Assert.That(() => authService.Login(user), Throws.Nothing);
        Assert.That(authService.GetDb.ContainsKey(user));
    }
}