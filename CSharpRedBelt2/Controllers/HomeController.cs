using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSharpRedBelt2.Models;

namespace CSharpRedBelt2.Controllers;

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

    [SessionCheck]
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
            List<Post> EveryPost = _context.Posts
            .Include(w => w.Creator)
            .Include(w => w.LikedPosts)
            .ThenInclude(a => a.User)
            .ToList();   
        MyViewModel MyModels = new MyViewModel
        {
            AllPosts = EveryPost
        };
        // ViewBag.AllCoupons = EveryCoupon;
        ViewBag.UserId = (int)HttpContext.Session.GetInt32("uuid");
        
        return View("Dashboard", MyModels);
    }

    [SessionCheck]
    [HttpGet("/post/new")]
    public IActionResult NewPost()
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }   
        ViewBag.UserId = (int)HttpContext.Session.GetInt32("uuid");
        return View("NewPost");
    }

    [SessionCheck]
    [HttpPost("/Create")]
    public IActionResult CreatePost(Post newPost)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        newPost.CreatorId = (int)HttpContext.Session.GetInt32("uuid");
        if(ModelState.IsValid)
        {
            
            _context.Add(newPost);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View("NewPost", newPost);
    }

    [SessionCheck]
    [HttpGet("/post/{id}")]
    public IActionResult ShowOnePost(int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        Post thisPost = _context.Posts
        .Include(w => w.Creator)
        .Include(u => u.LikedPosts)
        .ThenInclude(a => a.User)
        .FirstOrDefault(p => p.PostId ==id);
        ViewBag.thisPost = thisPost;
        
        return View("ShowOnePost", thisPost);
    }

    [SessionCheck]
    [HttpGet("/like/{id}")]
    public IActionResult LikePost (int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        LikedPost likedPost = new LikedPost();
        likedPost.UserId = (int)HttpContext.Session.GetInt32("uuid");
        likedPost.PostId = id;
        _context.Likes.Add(likedPost);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [SessionCheck]
    [HttpGet("/unlike/{id}")]
    public IActionResult UnLike (int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        LikedPost likedPost = _context.Likes.FirstOrDefault(a => a.LikedPostId ==id);
        // likedPost.UserId = (int)HttpContext.Session.GetInt32("uuid");
        likedPost.PostId = id;
        _context.Likes.Remove(likedPost);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }
    [SessionCheck]
    [HttpGet("/delete/post/{id}")]
    public IActionResult DeletePost(int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        Post PostToDelete = _context.Posts.SingleOrDefault(c => c.PostId == id);
        _context.Posts.Remove(PostToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [SessionCheck]
    [HttpGet("/post/edit/{id}")]
    public IActionResult PostEdit(int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        Post? CurrentPost = _context.Posts.SingleOrDefault(c => c.PostId == id);
        if(CurrentPost == null)
        {
            return RedirectToAction();
        }
        return View("PostEdit", CurrentPost);
    }

    [SessionCheck]
    [HttpPost("/post/update/{id}")]
    public IActionResult PostUpdate(Post newPost, int id)
    {
        if(HttpContext.Session.GetInt32("uuid") == null)
        {
            return Redirect("Index");
        }
        Post? OldPost = _context.Posts.FirstOrDefault(i => i.PostId == id);
        if(OldPost == null)
        {
            return RedirectToAction("Dashboard");
        }
        if(!ModelState.IsValid)
        {
            return PostEdit(id);
        }
        OldPost.Title = newPost.Title;
        OldPost.Image = newPost.Image;
        OldPost.Medium = newPost.Medium;
        OldPost.forSale = newPost.forSale;
        _context.SaveChanges();
        return ShowOnePost(id);
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
