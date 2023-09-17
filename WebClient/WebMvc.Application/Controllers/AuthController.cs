using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMvc.Application.Models;
using WebMvc.Application.Services;
using WebMvc.Application.Services.AuthService;

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
            var userInfo = await _authService.LoginAsync(user);

            Response.Cookies.Append("token", userInfo.Token, new CookieOptions
            {
                Secure = true
            });
            Response.Cookies.Append("refresh", userInfo.RefreshToken, new CookieOptions
            {
                Secure = true
            });
            Response.Cookies.Append("username", user.Login, new CookieOptions
            {
                Secure = true
            });
            Response.Cookies.Append("uuid", userInfo.Uuid.ToBase64(), new CookieOptions
            {
                Secure = true
            });

            return RedirectToAction("MyFiles", "Home");
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
            await _authService.RegisterAsync(user);

            return RedirectToAction("Login", "Auth");
        }
        catch (Exception e)
        {
            return View("_Error", e.Message);
        }
    }
}