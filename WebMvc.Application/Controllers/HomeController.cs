using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebMvc.Application.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

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