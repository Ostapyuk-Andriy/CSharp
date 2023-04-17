using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

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
        List<Dish> AllDishes = _context.Dishes.ToList(); 
        return View("Index", AllDishes);
    }
    [HttpGet("/Dish/New")]
    public IActionResult DishNew()
    {
        return View("DishNew");
    }
    [HttpPost("/Dish/Create")]
    public IActionResult DishCreate(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return View("DishNew");
        }
        _context.Dishes.Add(newDish);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    [HttpGet("/Dish/{id}")]    
    public IActionResult ShowDish(int id)    
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(a => a.DishId == id);            
        return View("OneDish", OneDish);  
    }
    [HttpGet("/Dish/{id}/edit")]
    public IActionResult EditDish(int id)
    {
        Dish? DishToEdit = _context.Dishes.FirstOrDefault(b => b.DishId == id);
        if(DishToEdit == null){
            return RedirectToAction("Index");
        }
        return View("EditDish", DishToEdit);
    }
    
    [HttpPost("/Dish/{DishId}/update")]
    public IActionResult UpdateDish(Dish editedDish, int DishId)
    {
        if(!ModelState.IsValid)
        {
            return EditDish(DishId);
        }
        Dish? OldDish = _context.Dishes.FirstOrDefault(i => i.DishId == DishId);
        if(OldDish == null)
        {
            return RedirectToAction("Index");
        }
        OldDish.DishName = editedDish.DishName;
        OldDish.ChefName = editedDish.ChefName;
        OldDish.Calories = editedDish.Calories;
        OldDish.Tastiness = editedDish.Tastiness;
        OldDish.Description = editedDish.Description;
        _context.SaveChanges();
        return ShowDish(DishId);
    }

    [HttpPost("/Dish/{DishId}/Delete")]
    public IActionResult DishDelete(int DishId)
    {
        Dish? DishToDelete = _context.Dishes.SingleOrDefault(c => c.DishId == DishId);
        if(DishToDelete == null)
        {
            return RedirectToAction("Index");
        }
        _context.Dishes.Remove(DishToDelete);
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
