using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

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
        ViewBag.AllChefs = _context.Chefs.ToList();
        ViewBag.AllChefs = _context.Chefs.Include(a => a.CreatedMessages).ToList();

        return View();
    }


    [HttpGet("chefs/new")]
    public IActionResult AddChef()
    {
        return View();
    }
    [HttpGet("dishes/new")]
    public IActionResult AddDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View();
    }
    
    [HttpGet("dishes")]
    public IActionResult dishes()
    {
        ViewBag.AllDishes = _context.Dishes.ToList();
        ViewBag.AllDishes = _context.Dishes.Include(a => a.Creator).ToList();

        return View();
    }
    



    [HttpPost("Add_chef")]
    
    public IActionResult Add_chef( Chef newChef)
    
    {
        if(ModelState.IsValid)
        {
        
            if( (DateTime.Now.Year -newChef.Date.Year) > 18 )
            {
                // we can add to the database
                _context.Add(newChef);
                _context.SaveChanges();
                return RedirectToAction("AddChef");
            }       
            else   {
                ModelState.AddModelError("Date", "Age dhould be > 18");
            } 
        }
            
        else {
            return View("AddChef");
        }

        return View("AddChef");

    }

    [HttpPost("Add_dish")]
    
    public IActionResult Add_dish( Dish newDish)
    
    {
        if(ModelState.IsValid)
        {
        
             // we can add to the database
                _context.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("AddDish");
            
        }
            
        else {
            ViewBag.AllChefs = _context.Chefs.ToList();
            ViewBag.AllDishes = _context.Dishes.Include(a => a.Creator).ToList();

            return View("AddDish");
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
