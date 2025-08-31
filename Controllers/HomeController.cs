using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS.Models;
using LMS.Models.Dao.Interface;
using LMS.Models.Entities;

namespace LMS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeDao _employeeDao;

    public HomeController(ILogger<HomeController> logger, IEmployeeDao employeeDao)
    {
        _logger = logger;
        _employeeDao = employeeDao;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string id, string pass)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pass)) return RedirectToAction("Failed");
        
        EmployeeEntity employee = new EmployeeEntity { Id = id, Pass = pass };
        EmployeeEntity result = _employeeDao.Find(employee);
        
        if (result == null) return RedirectToAction("Failed");
        
        ViewData["name"] = result.Name;
        return View();
    }

    public IActionResult Failed()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}