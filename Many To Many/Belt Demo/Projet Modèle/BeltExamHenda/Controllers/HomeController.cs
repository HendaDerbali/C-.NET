using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BeltExamHenda.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http; // About Session


namespace BeltExamHenda.Controllers;

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

    // Registration :
    [HttpPost("user/register")]
    public IActionResult Register(User newUser)
    {
        ViewBag.NotLogIn = true;
        if (ModelState.IsValid)
        {

            // Verify if the email is unique
            // hash the password before adding it to the database
            if (_context.Users.Any(u => u.Email == newUser.Email))
            {

                // error message
                ModelState.AddModelError("Email", "Email already in use!");
                return View("Index");
            }
            // Hash the password before adding it to the database
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId);

            return RedirectToAction("Dashboard");
        }
        else
        {
            return View("Index");
        }
    }
    // Login
    [HttpPost("user/login")]
    public IActionResult Login(LogUser LoginUser)
    {
        ViewBag.NotLogIn = true;
        if (ModelState.IsValid)
        {

            // Verify the email in our Database
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == LoginUser.LogEmail);
            // If no user exists with provided email
            if (userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            // then verify if the password matches what's in the database
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(LoginUser, userInDb.Password, LoginUser.LogPassword);
            if (result == 0)
            {
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);

            return RedirectToAction("Dashboard");
        }
        else
        {
            return View("Index");
        }
    }

    // Dashboard : Dont forget to change it later relatively to exam 

    // Other routes in exam




















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
