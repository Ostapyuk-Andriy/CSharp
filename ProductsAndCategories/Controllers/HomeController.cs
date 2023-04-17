using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers;

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
        MyViewModel MyModel = new MyViewModel
        {
            AllProducts = _context.Products.ToList()
        };
        return View(MyModel);
    }

    [HttpPost("/Product/Create")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if(ModelState.IsValid)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } else {
            MyViewModel MyModel = new MyViewModel
            {
                AllProducts = _context.Products.ToList()
            };
            return View("Index", MyModel);
        }
    }

    [HttpGet("/Product/Show")]
    public IActionResult ShowProduct()
    {
        List<Product> EveryProduct = _context.Products.ToList();
        ViewBag.AllProducts = _context.Products.ToList();
        return View("Index");
    }
    
    [HttpGet("/Product/Show/{id}")]
    public IActionResult ProductShowOne(int id)
    {
            Product currentProduct = _context.Products
            .Include(c => c.Associations)
            .ThenInclude(c => c.Category)
            .SingleOrDefault(c => c.ProductId == id);
        MyViewModel MyModel = new MyViewModel
        {
            Product = currentProduct
        };
        List<Category> productHasCategories = currentProduct.Associations.Select(a => a.Category).ToList();

        ViewBag.AllCat = _context.Categories.Where(w => !productHasCategories.Contains(w)).ToList();
        ViewBag.ProductId = id;
        return View("ProductShowOne", MyModel);
    }

    [HttpPost("/AddCatToProd")]
    public IActionResult AddCatToProd(Association newAssociation)
    {
        if(!ModelState.IsValid || _context.Associations.Any(a => a.ProductId == newAssociation.ProductId && a.CategoryId == newAssociation.CategoryId))
        {
            return ProductShowOne(newAssociation.ProductId);
        }
        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return Redirect("/Product/Show/"+newAssociation.ProductId );
    }

        [HttpGet("/Category")]
    public IActionResult Category()
    {
        MyViewModel MyModel = new MyViewModel
        {
            AllCategories = _context.Categories.ToList()
        };
        return View(MyModel);
    }

    [HttpPost("/Category/Create")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if(ModelState.IsValid)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Category");
        } else {
            MyViewModel MyModel = new MyViewModel
            {
                AllCategories = _context.Categories.ToList()
            };
            return View("Category", MyModel);
        }
    }

    [HttpGet("/Category/Show")]
    public IActionResult ShowCategory()
    {
        ViewBag.AllCategories= _context.Categories.ToList();
        
        return Category();
    }

    [HttpGet("/Category/Show/{id}")]
    public IActionResult CategoryShowOne(int id)
    {
            Category currentCategory = _context.Categories
            .Include(c => c.Products)
            .ThenInclude(c => c.Product)
            .SingleOrDefault(c => c.CategoryId == id);
        MyViewModel MyModel = new MyViewModel
        {
            Category = currentCategory
        };
        List<Product> categoryHasProducts = currentCategory.Products.Select(a => a.Product).ToList();
        ViewBag.AllProd = _context.Products.Where(w => !categoryHasProducts.Contains(w)).ToList();
        ViewBag.CategoryId = id;
        return View("CategoryShowOne", MyModel);
    }

    [HttpPost("/AddProdToCat")]
    public IActionResult AddProdToCat(Association newAssociation)
    {
        if(!ModelState.IsValid || _context.Associations.Any(a => a.CategoryId == newAssociation.CategoryId && a.ProductId == newAssociation.ProductId))
        {
            return  CategoryShowOne(newAssociation.CategoryId);
        }
        _context.Associations.Add(newAssociation);
        _context.SaveChanges();
        return Redirect("/Category/Show/"+newAssociation.CategoryId );
    }


    [HttpGet("/privacy")]
    public IActionResult Privacy()
    {
        return View("Privacy");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
