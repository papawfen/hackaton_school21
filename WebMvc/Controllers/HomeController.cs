using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public string Login(string login, string password)
        => $"{login} {password}";


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