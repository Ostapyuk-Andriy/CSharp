using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers;

public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;
    private MyContext _context;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/Chef/New")]
    public IActionResult ChefNew()
    {
        return View("~/Views/Chef/_ChefCreateForm.cshtml");
    }
    [HttpPost("/Chef/Create")]
    public IActionResult ChefCreate(Chef newChef)
    {
        if(!ModelState.IsValid)
        {
            return View("~/Views/Chef/_ChefCreateForm.cshtml");
        }
        _context.Chefs.Add(newChef);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("/Chef/Show")]
    public IActionResult ChefShow()
    {
        MyViewModel MyModel = new MyViewModel
        {
            AllChefs = _context.Chefs.ToList()
        };
        return View("~/Views/Home/Index.cshtml", MyModel);
    }

    [HttpGet("/Chef/{id}")]    
    public IActionResult ShowChef(int id)    
    {
        Chef? OneChef = _context.Chefs.FirstOrDefault(a => a.ChefId == id);            
        return View("OneChef", OneChef);  
    }
    [HttpGet("/Chef/{id}/edit")]
    public IActionResult EditChef(int id)
    {
        Chef? ChefToEdit = _context.Chefs.FirstOrDefault(b => b.ChefId == id);
        if(ChefToEdit == null){
            return RedirectToAction("Index");
        }
        return View("EditChef", ChefToEdit);
    }
    
    [HttpPost("/Chef/{ChefId}/update")]
    public IActionResult UpdateChef(Chef editedChef, int ChefId)
    {
        if(!ModelState.IsValid)
        {
            return EditChef(ChefId);
        }
        Chef? OldChef = _context.Chefs.FirstOrDefault(i => i.ChefId == ChefId);
        if(OldChef == null)
        {
            return RedirectToAction("Index");
        }
        OldChef.FirstName = editedChef.FirstName;
        OldChef.LastName = editedChef.LastName;
        OldChef.DateOfBirth = editedChef.DateOfBirth;
        _context.SaveChanges();
        return ShowChef(ChefId);
    }

    [HttpPost("/Chef/{ChefId}/Delete")]
    public IActionResult ChefDelete(int ChefId)
    {
        Chef? ChefToDelete = _context.Chefs.SingleOrDefault(c => c.ChefId == ChefId);
        if(ChefToDelete == null)
        {
            return RedirectToAction("Index");
        }
        _context.Chefs.Remove(ChefToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}