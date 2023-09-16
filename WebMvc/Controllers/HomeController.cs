using Microsoft.AspNetCore.Mvc;
using WebMvc.Models.User;
using Microsoft.Extensions.Logging;

namespace WebMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public string Login(User user)
        => $"{user.Login} {user.Password}";

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public string Register(User user)
        => $"{user.Login} {user.Password}";

    [HttpGet]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public IActionResult MyFiles()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SharedFiles()
    {
        return View();
    }
}