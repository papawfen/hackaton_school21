using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMvc.Services;

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
    public IActionResult Login(string login, string password)
    {
        _logger.LogInformation("User clicked login");
        AuthService authService = new AuthService();
        _logger.LogWarning(authService.Echo(login));
        return View();
    }

    [HttpGet]
    public IActionResult About()
    {
        return View();
    }

    public IActionResult MyFiles()
    {
        return View();
    }

    public IActionResult SharedFiles()
    {
        return View();
    }
}