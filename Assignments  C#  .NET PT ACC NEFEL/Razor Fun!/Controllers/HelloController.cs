using Microsoft.AspNetCore.Mvc;
namespace YourNamespace.Controllers;     //be sure to use your own project's namespace!
    public class HelloController : Controller   //remember inheritance??
    {

    [HttpGet]
    [Route("")]
    public ViewResult Index()
    {
        return View();
    }
}