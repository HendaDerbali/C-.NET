using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

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
    // Select All from Dishes
    ViewBag.Alldishes = _context.Dishes.OrderBy(a => a.Name).ToList();
    
    return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("dishes/new")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet("/Home")]
    public IActionResult Home()
    {
    ViewBag.Alldishes = _context.Dishes.OrderBy(a => a.Name).ToList();
    return View("Index");
    }
    
    [HttpPost("/add")]
    public IActionResult Add(Dish newDish)
    {
        // We add this to the database so long it's correct
        if (ModelState.IsValid)
        {
            // we can add to the database
            // and pass this object to the .Add() method
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Add");
        }
        else
        {
            return View("Add");
        }
    }
    [HttpGet("dishes/{dishId}")]
    public IActionResult Display(int dishId)
    {
        Dish oneDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId); 
        if (oneDish == null)
            return RedirectToAction("Index");

    return View(oneDish);   
    }

    [HttpGet("delete/{dishId}")]
    public IActionResult Delete(int dishId)
    {
        Dish RetrievedDish = _context.Dishes
        .SingleOrDefault(d => d.DishId == dishId);
        _context.Dishes.Remove(RetrievedDish);
        _context.SaveChanges();
    
        return RedirectToAction ("Index");
    }

    [HttpGet("edit/{dishId}")]
    public IActionResult Edit(int dishId)
    {
        // we Need to find the item
        Dish DishToEdit = _context.Dishes.FirstOrDefault(b => b.DishId == dishId);
        return View(DishToEdit);
    }

    [HttpPost("{dishId}/update")]
        public IActionResult Update(Dish dish, int dishId)
        {
            Dish toUpdate = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if(ModelState.IsValid)
            {
                toUpdate.Name = dish.Name;
                toUpdate.Chef = dish.Chef;
                toUpdate.Tastiness = dish.Tastiness;
                toUpdate.Calories = dish.Calories;
                toUpdate.TEXT = dish.TEXT;
                toUpdate.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("Display", dish);
            }
            else{
                return View("Edit", dish);
            }
        
        }
        





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
