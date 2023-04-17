using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost("/AddUser")]
    public IActionResult AddUser(User newUser)
    {
        if(!ModelState.IsValid)
        {
            return View("Index");
        }
        HttpContext.Session.SetString("userName", newUser.Name);
        return RedirectToAction("OneUser");
    }
    [HttpGet("/OneUser")]
    public IActionResult OneUser(User newNumber)
    {
        if(!HttpContext.Session.Keys.Contains("userName"))
        {
            return RedirectToAction("Index");
        }
        if(!HttpContext.Session.Keys.Contains("userNumber")){
            HttpContext.Session.SetInt32("userNumber", newNumber.Number=22);
        }
        
        return View("Dashboard");
    }
    [HttpGet("Add")]
    public IActionResult Add(User newNumber)
    {
        int? val = HttpContext.Session.GetInt32("userNumber");
        val++;
        HttpContext.Session.SetInt32("userNumber", val ?? 0);
        return RedirectToAction("OneUser");
    }
    [HttpGet("Subtract")]
    public IActionResult Subtract(User newNumber)
    {
        int? val = HttpContext.Session.GetInt32("userNumber");
        val--;
        HttpContext.Session.SetInt32("userNumber", val ?? 0);
        return RedirectToAction("OneUser");
    }
    [HttpGet("Multiply")]
    public IActionResult Multiply(User newNumber)
    {
        int? val = HttpContext.Session.GetInt32("userNumber");
        val*=2;
        HttpContext.Session.SetInt32("userNumber", val ?? 0);
        return RedirectToAction("OneUser");
    }
    [HttpGet("Random")]
    public IActionResult Random(User newNumber)
    {
        int? val = HttpContext.Session.GetInt32("userNumber");
        Random rand = new Random();
        int newRandomVal = rand.Next(1,11);
        val+= newRandomVal;
        HttpContext.Session.SetInt32("userNumber", val ?? 0);
        return RedirectToAction("OneUser");
    }
    [HttpGet("ClearSession")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

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
