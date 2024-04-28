using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers;

public class HelloController : Controller
{

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        
        return View("Index");
    }
    [HttpPost("submit")]
    public IActionResult submit(string Name, string Location, string Language, string Comment)

    {
        ViewBag.Name= Name;
        ViewBag.Location = Location;
        ViewBag.Language = Language;
        ViewBag.Comment =  Comment;
        return View("Result");
    }


[HttpGet]
    [Route("result")]
    public ViewResult result()
    {
        
        return View("Result");
    }

}