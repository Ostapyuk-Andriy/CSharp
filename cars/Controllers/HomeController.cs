using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cars.Models;

namespace cars.Controllers;

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
    [HttpGet("/Car/New")]
    public IActionResult CarNew()
    {
        return View("CarNew");
    }
    [HttpPost("/Car/Create")]
    public IActionResult CarCreate(Car newCar)
    {
        if(!ModelState.IsValid)
        {
            return View("CarNew");
        }
        _context.Cars.Add(newCar);
        _context.SaveChanges();

        return RedirectToAction("Index");
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
