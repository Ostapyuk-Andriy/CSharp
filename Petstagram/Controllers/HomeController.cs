using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Petstagram.Models;

namespace Petstagram.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("/Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost("/AddPet")]
    public IActionResult AddPet(Pet newPet)
    {
        if(!ModelState.IsValid)
        {
            return View("Index");
        }
        // if (newPet.Type == "guinea pig")
        // {
        //     ViewBag.SecretMessage = "You found the hidden Egg!";
        //     return View("Index");
        // }
        System.Console.WriteLine($"{newPet.Name} in a(n) {newPet.Age} year old petwith {newPet.HairColor} hair");
            // ViewBag.PetName = PetName;
            // ViewBag.PetType = PetType;
            // ViewBag.Age = Age;
            // ViewBag.HairColor = HairColor;

        // Pet tylersPet = new Pet(){
        //     Name = PetName,
        //     Type = PetType,
        //     Age = Age,
        //     HairColor = HairColor
        // };
        // return Redirect("/");
        // Need to have a RedirectToActionResult insted of RedirectResult
        HttpContext.Session.SetString("petName", newPet.Name);
        HttpContext.Session.SetString("petType", newPet.Type);
        HttpContext.Session.SetInt32("petAge", newPet.Age ?? 0);
        HttpContext.Session.SetString("petHairColor", newPet.HairColor);

        return RedirectToAction("OnePet");
    }

    [HttpGet("/OnePet")]
    public IActionResult OnePet()
    {
        if(!HttpContext.Session.Keys.Contains("petName"))
        {
            return RedirectToAction("Index");
        }
        // Pet newPet = new Pet()
        // {
        //     Name: HttpContext.Session.GetString();
        // };
        return View("ViewPet");
    }
    [HttpGet("ClearSession")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet("{**patch}")]
    public IActionResult Unknown()
    {
        return RedirectToAction("Index");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
