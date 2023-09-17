using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMvc.Application.Models;
using WebMvc.Application.Services;
using WebMvc.Application.Services.AuthService;

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
        // fix path
        var images = new List<MediaEntry>();
        foreach (var file in Directory.EnumerateFiles($"{Directory.GetCurrentDirectory()}" + "/wwwroot/Cringe"))
        {
            var realFile = new FileInfo(file).Name;
            images.Add(new MediaEntryBuilder().SetPreviewIconPath($"{realFile}").SetName(realFile).Build());
        }

        ViewBag.Images = images;
        return View();
    }

    [HttpGet]
    public IActionResult SharedFiles()
    {
        return View();
    }
}