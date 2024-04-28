using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsandCategories.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductsandCategories.Controllers;

public class HomeController : Controller
{
    private MyContext _context;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.AllProducts = _context.Products.ToList();
        return View();
    }

    [HttpGet("categories")]
    public IActionResult Categories()
    {
        ViewBag.AllCategories = _context.Categories.ToList();
        return View();
    }

    [HttpGet("products/{ProductId}")]
    public IActionResult ADDProduct(int ProductId)
{
    ViewBag.AllProducts = _context.Products.Where(p => p.ProductId == ProductId).ToList();
    ViewBag.AllCategories = _context.Categories.ToList();
    //New to add in this case  :
    ViewBag.ProductId = ProductId; 
    

    ViewBag.AllProducts = _context.Products.Include(a => a.MyCategories).ThenInclude(d => d.Category).Where(p => p.ProductId == ProductId).ToList();


    return View();
}

[HttpGet("categories/{CategoryId}")]
    public IActionResult ADDCategory(int CategoryId)
{
    ViewBag.AllCategories = _context.Categories.Where(p => p.CategoryId == CategoryId).ToList();
    ViewBag.AllProducts = _context.Products.ToList();
    //New to add in this case  :
    ViewBag.CategoryId = CategoryId; 
    

    ViewBag.AllCategories = _context.Categories.Include(a => a.MyProducts).ThenInclude(d => d.Product).Where(p => p.CategoryId == CategoryId).ToList();


    return View();
}



    [HttpPost("AddProduct")]
    
    public IActionResult AddProduct( Product newProduct)
    
    {
        if(ModelState.IsValid)
        {
        
             // we can add to the database
                _context.Add(newProduct);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
        }
            
        else {
            ViewBag.AllProducts = _context.Products.ToList();
            return View("Index");
        }

    }

    [HttpPost("AddCategory")]
    
    public IActionResult AddCategory( Category newCategory)
    
    {
        if(ModelState.IsValid)
        {
        
             // we can add to the database
                _context.Add(newCategory);
                _context.SaveChanges();
                return RedirectToAction("Categories");
            
        }
            
        else {
            ViewBag.AllCategories = _context.Categories.ToList();
            return View("Categories");
        }

    }

[HttpPost("AddProd/{CategoryId}")]
public IActionResult AddCat(int CategoryId, Association newAssociation)
{
    if (ModelState.IsValid)
    {
    
        // Add the new association to the database
        _context.Add(newAssociation);
        _context.SaveChanges();

        // Redirect back to the product details page
        return RedirectToAction("ADDCategory", new { CategoryId });
    }
    else
    {
        // If there are validation errors, return to  the ADDProduct view
        ViewBag.AllCategories = _context.Categories.Where(p => p.CategoryId == CategoryId).ToList();
        ViewBag.AllProducts = _context.Products.ToList();
        ViewBag.ProductId = CategoryId; // Pass the ProductId to the view
        return View("ADDCategory");
    }
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
