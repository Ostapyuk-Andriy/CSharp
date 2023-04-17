using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSharpRedBelt.Models;

namespace CSharpRedBelt.Controllers;

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
        return View("Index");
    }

    // [SessionCheck]
    // [HttpGet("/navbar/{id}")]
    // public IActionResult NavBar( int id)
    // {
    //     if(HttpContext.Session.GetInt32("uuid") == null)
    //     {
    //         return Redirect("Index");
    //     }
    //     MyViewModel MyModel = new MyViewModel
    //     {
    //         User = _context.Users.FirstOrDefault(a => a.UserId ==id)
    //     };
    //     // User thisUser =_context.Users.FirstOrDefault(w => w.UserId ==id);
        
    //     return View("_Navbar", MyModel);
    // }

    [SessionCheck]
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
            List<Coupon> EveryCoupon = _context.Coupons
            .Include(w => w.Creator)
            .Include(w => w.CouponsUsed)
            .ThenInclude(a => a.User)
            .ToList();   
        MyViewModel MyModels = new MyViewModel
        {
            AllCoupons = EveryCoupon
        };
        // ViewBag.AllCoupons = EveryCoupon;
        ViewBag.UserId = (int)HttpContext.Session.GetInt32("uuid");
        return View("Dashboard", MyModels);
    }

    [SessionCheck]
    [HttpGet("/coupon/new")]
    public IActionResult NewCoupon()
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }   
        ViewBag.UserId = (int)HttpContext.Session.GetInt32("uuid");
        return View("NewCoupon");
    }

    [SessionCheck]
    [HttpPost("/Create")]
    public IActionResult CreateCoupon(Coupon newCoupon)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        newCoupon.CreatorId = (int)HttpContext.Session.GetInt32("uuid");
        if(ModelState.IsValid)
        {
            
            _context.Add(newCoupon);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View("NewCoupon", newCoupon);
    }

    [SessionCheck]
    [HttpGet("/user/{id}")]
    public IActionResult ShowOneUser(int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }

        User thisUser = _context.Users
        .Include(u => u.CreatedCoupons)
        .ThenInclude(a => a.Coupon)
        .FirstOrDefault(p => p.UserId ==id);

        ViewBag.thisUser = thisUser;
        
        List<Coupon> EveryCoupon = _context.Coupons.Where(w => w.CreatorId == id).ToList();
        ViewBag.EveryCoupon = EveryCoupon;

        return View("ShowOneUser", thisUser);
    }

    [SessionCheck]
    [HttpGet("/use/{id}")]
    public IActionResult UseCoupon (int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        UsedCoupon usedCoupon = new UsedCoupon();
        usedCoupon.UserId = (int)HttpContext.Session.GetInt32("uuid");
        usedCoupon.CouponId = id;
        _context.Usages.Add(usedCoupon);
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
