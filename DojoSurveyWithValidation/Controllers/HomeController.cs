using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoSurveyWithValidation.Models;

namespace DojoSurveyWithValidation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

[HttpPost("process")]
    public IActionResult process(Dojo newDojo)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Result", newDojo);
        }
        else{
            return View("Index");
        }
    }

    [HttpGet("Result")]
    public IActionResult Result(Dojo newDojo)
    {
        ViewBag.Dojo = newDojo;
        return View(newDojo);
    }







    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
