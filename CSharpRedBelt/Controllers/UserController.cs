using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CSharpRedBelt.Models;

namespace CSharpRedBelt.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/User/Logout")]
    public IActionResult UserLogout()
    {
        HttpContext.Session.Remove("uuid");
        return RedirectToAction ("Index", "Home");
    }

    [HttpPost("User/Login")]
    public IActionResult UserLogin(UserLogin newUser)
    {
        if(!ModelState.IsValid)
        {
            return View("~/Views/Home/Index.cshtml");
        }
        User? userInDB = _context.Users.SingleOrDefault(u => u.Email == newUser.EmailLogin);
        if(userInDB == null)
        {
            ModelState.TryAddModelError("EmailLogin", "Invalid credentials");
            return View("~/Views/Home/Index.cshtml");
        }
        PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
        var result = hasher.VerifyHashedPassword(newUser, userInDB.Password, newUser.PasswordLogin);
        if(result == 0)
        {
            ModelState.TryAddModelError("EmailLogin", "Invalid credentials");
            return View("~/Views/Home/Index.cshtml");
        }
        HttpContext.Session.SetInt32("uuid", userInDB.UserId);
        return RedirectToAction ("Dashboard", "Home");
    }

    [HttpGet("/User/New")]
    public IActionResult UserNew()
    {
        return View("UserNew");
    }

    [HttpPost("/User/Create")]
    public IActionResult UserCreate(User newUser)
    {
        if(!ModelState.IsValid)
        {
            return View("~/Views/Home/Index.cshtml");
        }

        PasswordHasher<User> hasher = new PasswordHasher<User>();
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);

        _context.Users.Add(newUser);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("uuid", newUser.UserId);
        
        return RedirectToAction("Dashboard", "Home");
    }

    [HttpGet("User/{id}/Edit")]
    public IActionResult UserEdit(int id)
    {
        User? currentUser = _context.Users.SingleOrDefault(c => c.UserId == id);
        if(currentUser == null)
        {
            return RedirectToAction("Index");
        }
        return View("UserEdit", currentUser);
    }

    [HttpPost("/User{id}/Update")]
    public IActionResult UserUpdate(User newUser, int id)
    {
        if(!ModelState.IsValid)
        {
            return UserEdit(id);
        }
        User? oldUser = _context.Users.SingleOrDefault(c => c.UserId == id);
        if(oldUser ==null)
        {
            return RedirectToAction("Index");
        }
        oldUser.UpdatedAt = newUser.UpdatedAt;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("User/{id}/Delete")]
    public IActionResult UserDelete(int id)
    {
        User? UserToDelete = _context.Users.SingleOrDefault(c => c.UserId == id);
        if(UserToDelete == null)
        {
            return RedirectToAction("Index");
        }
        _context.Users.Remove(UserToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}