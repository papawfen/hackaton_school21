using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMvc.Application.Models.User;
using WebMvc.Application.Services;

namespace WebMvc.Application.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger,
        IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        try
        {
            var userInfo = await _authService.TryAuthenticate(user);

            Response.Cookies.Append("jwt", BitConverter.ToString(userInfo.JwtToken));
            Response.Cookies.Append("refresh", BitConverter.ToString(userInfo.RefreshToken));

            return NoContent();
        }
        catch (Exception e)
        {
            return View("_Error", e.Message);
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        try
        {
            var userInfo = await _authService.TryRegister(user);

            Response.Cookies.Append("jwt", BitConverter.ToString(userInfo.JwtToken));
            Response.Cookies.Append("refresh", BitConverter.ToString(userInfo.RefreshToken));

            return NoContent();
        }
        catch (Exception e)
        {
            return View("_Error", e.Message);
        }
    }
}