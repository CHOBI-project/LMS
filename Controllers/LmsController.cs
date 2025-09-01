using System.Diagnostics;
using LMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers;

public class LmsController : Controller
{
    private readonly ILogger<LmsController> _logger;

    public LmsController(ILogger<LmsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Search(string keyword)
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}