using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using loginreg.Models;

namespace loginreg.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }
    [SessionCheck]
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        return View("Dashboard");
    }

    [HttpGet("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
