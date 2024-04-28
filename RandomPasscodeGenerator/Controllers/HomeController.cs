using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscodeGenerator.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscodeGenerator.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Built my session Henda:
    //     HttpContext.Session.SetString("User", "Henda");
    //     string? user = HttpContext.Session.GetString("User");
    //     Console.WriteLine(user);
        return View();
    }

[HttpPost("process")]
public IActionResult Process(string pwd)
{
    // Initialize the count to 0 if it doesn't exist in the session
    int count = HttpContext.Session.GetInt32("Count") ?? 0;
    
    if (!string.IsNullOrEmpty(pwd))
    {
        count++;
        Console.WriteLine(count);

        HttpContext.Session.SetString("pwd", pwd);

        // Update the count in the session
        HttpContext.Session.SetInt32("Count", count);

        // Set ViewBag.Count to the updated count
        ViewBag.Count = count;

        
    }
    return View("Index");
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

