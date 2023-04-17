using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    private MyContext _context;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/Dish/New")]
    public IActionResult DishNew()
    {
        List<Chef> AllChefs = _context.Chefs.Include(c => c.AllDishes).ToList();
        ViewBag.AllChefs = AllChefs;
        return View("_DishCreateForm");
    }
    [HttpPost("/Dish/Create")]
    public IActionResult DishCreate(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return DishNew();
        }
        _context.Dishes.Add(newDish);
        _context.SaveChanges();

        return DishShow();
    }

        [HttpGet("/Dish/Show")]
    public IActionResult DishShow()
    {
        MyViewModel MyModel = new MyViewModel
        {
            AllDishes = _context.Dishes.Include(C => C.Chef).ToList()
        };
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View("DishShow", MyModel);
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
        OldDish.Calories = editedDish.Calories;
        OldDish.Tastiness = editedDish.Tastiness;
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
}