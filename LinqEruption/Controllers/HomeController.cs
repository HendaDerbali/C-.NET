using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinqEruption.Models;

namespace LinqEruption.Controllers;

public class HomeController : Controller
{

List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46,"Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};










    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
         //Get all the data (Eruptions) from database :
        List<Eruption> eruptionsFromData = eruptions.ToList();
        ViewBag.eruptionsFromData = eruptionsFromData ;
        //Get all the data (Eruptions) from database but ordered:
        List<Eruption> orderedDataByYearDescending = eruptions.OrderByDescending(o => o.Year).ToList();
        ViewBag.orderedDataByYearDescending = orderedDataByYearDescending ;
        //First eruption
    List<Eruption> FirstEruption = eruptions.Where(o => o.Location == "Hawaiian Is").ToList();

    if (FirstEruption.Count != 0)
    {
        ViewBag.FirstEruption = FirstEruption;
    }
    else
    {
        return Content("No Hawaiian Is Eruption found.");
    }
    // Find the first eruption that is after the year 1900 AND in "New Zealand", then print it.
    List<Eruption> EruptionAfter = eruptions.Where(o => o.Location == "New Zealand").Where(o => o.Year > 1900).ToList();
    ViewBag.EruptionAfter = EruptionAfter;
    // Find all eruptions where the volcano's elevation is over 2000m and print them.
    List<Eruption> EruptionElevation = eruptions.Where(o => o.ElevationInMeters > 2000).ToList();
    ViewBag.EruptionElevation = EruptionElevation;
    //Find all eruptions where the volcano's name starts with "L" and print them. Also print the number of eruptions found.
    List<Eruption> EruptionName = eruptions.Where(o => o.Volcano.StartsWith("L")).ToList();
    int nbr = EruptionName.Count;
    ViewBag.EruptionName = EruptionName;
    ViewBag.nbr = nbr;
    ViewBag.nbr = nbr;
    //Find the highest elevation, and print only that integer (Hint: Look up how to use LINQ to find the max!)
    int HighestElevation = eruptions.Max(o => o.ElevationInMeters);
    ViewBag.HighestElevation = HighestElevation;
    // Use the highest elevation variable to find a print the name of the Volcano with that elevation.
    Eruption ListWhereHighestElevation = eruptions.FirstOrDefault(o => o.ElevationInMeters == HighestElevation);
    string Name =  ListWhereHighestElevation.Volcano;
    ViewBag.Name = Name;



    

    return View();


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
