using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers;

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
        List<Wedding> EveryWedding = _context.Weddings
        .Include(w => w.WeddingAttendees)
        .ThenInclude(a => a.User)
        .ToList();
        ViewBag.AllWeddings = EveryWedding;
        ViewBag.UserId = (int)HttpContext.Session.GetInt32("uuid");
        return View("Dashboard");
    }
    [SessionCheck]
    [HttpGet("/wedding/new")]
    public IActionResult NewWedding()
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        return View("NewWedding");
    }

    [SessionCheck]
    [HttpPost("/Create")]
    public IActionResult CreateWedding(Wedding newWedding)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        newWedding.PlannerId = (int)HttpContext.Session.GetInt32("uuid");
        if(ModelState.IsValid)
        {
            
            _context.Add(newWedding);
            _context.SaveChanges();
            return Redirect("/wedding/"+newWedding.WeddingId);
        }
        return View("NewWedding", newWedding);
    }

    [HttpGet("/delete/wedding/{id}")]
    public IActionResult DeleteWedding(int id)
    {
        Wedding WeddingToDelete = _context.Weddings.SingleOrDefault(c => c.WeddingId == id);
        _context.Weddings.Remove(WeddingToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("/wedding/{id}")]
    public IActionResult ShowOneWedding(int id)
    {
        Wedding thisWedding =_context.Weddings.FirstOrDefault(w => w.WeddingId ==id);
        ViewBag.thisWedding = thisWedding;
        var weddingGuests = _context.Weddings
        .Include(w => w.WeddingAttendees)
        .ThenInclude(u => u.User)
        .FirstOrDefault(w => w.WeddingId == id);

        ViewBag.AllGuests = weddingGuests.WeddingAttendees;

        return View("ShowOneWedding");
    }

    [HttpGet("/rsvp/{id}")]
    public IActionResult Reserve (int id)
    {
        Attendance attendance = new Attendance();
        attendance.UserId = (int)HttpContext.Session.GetInt32("uuid");
        attendance.WeddingId = id;
        _context.Attendances.Add(attendance);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("/unrsvp/{id}")]
    public IActionResult UnReserve (int id)
    {
        Attendance attendance = _context.Attendances.FirstOrDefault(a => a.AttendanceId ==id);
        _context.Attendances.Remove(attendance);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
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
