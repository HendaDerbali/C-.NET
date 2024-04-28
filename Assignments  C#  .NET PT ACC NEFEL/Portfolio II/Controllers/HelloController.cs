using Microsoft.AspNetCore.Mvc;
namespace YourNamespace.Controllers;     //be sure to use your own project's namespace!
    public class HelloController : Controller   //remember inheritance??
    {
        //for each route this controller is to handle:
        [HttpGet]       //type of request
        [Route("")]     //associated route string (exclude the leading /)
        public ViewResult Index()
        {
            return View();
        }

    [HttpGet("projects")]      
        public ViewResult Projects()
        {
            return View("Projects");
        }

    [HttpGet("contact")]      
        public ViewResult Contact()
        {
            return View("Contact");
        }

    }

