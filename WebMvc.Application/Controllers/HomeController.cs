using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMvc.Application.Models;
using WebMvc.Application.Services;

namespace WebMvc.Application.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFileService _fileService;

    public HomeController(ILogger<HomeController> logger,
        IFileService fileService)
    {
        _logger = logger;
        _fileService = fileService;
    }

    [HttpGet]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public IActionResult MyFiles()
    {
        ViewBag.Images = new List<MediaEntry>
        {
            new MediaEntryBuilder().SetName("succesful").Build()
        };
        return View();
    }

    [HttpGet]
    public IActionResult SharedFiles()
    {
        return View();
    }
}