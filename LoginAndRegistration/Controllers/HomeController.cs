using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using LoginAndRegistration.Models;

namespace LoginAndRegistration.Controllers;

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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

[HttpPost("/Register")]
    public IActionResult Register(User newUser)
    {
        // We add this to the database so long it's correct
        if (ModelState.IsValid)
        {
            // Pass All Validation
            // Check if email exist or no :
            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");
                return View("Index");
            }
            // Initializing a PasswordHasher object, providing our User class as its type
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

            _context.Add(newUser);
            _context.SaveChanges();
            return RedirectToAction("Success");

        }
        else
        {
            return View("Index");
        }
    }

    [HttpGet("Success")]
    public IActionResult Success()
    {
        return View();
    }
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        return View("Index");
    }

    [HttpPost("/Login")]
    public IActionResult Login(LoginUser loginUser)
    {
        if (ModelState.IsValid)
        {
            // step 1 : find their email ad if we can't find it throw an error
            User userInDB = _context.Users.FirstOrDefault(a => a.Email == loginUser.LogEmail);
            if (userInDB == null)
            {
                // there was no Email in the database
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
            var result = Hasher.VerifyHashedPassword(loginUser, userInDB.Password, loginUser.LogPassword);
            if (result == 0)
            {
                // this is a problem, we did not the correct password
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            else
            {
                return RedirectToAction("Success");
            }
        }
        else
        {
            return View("Index");
        }
    }










    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
