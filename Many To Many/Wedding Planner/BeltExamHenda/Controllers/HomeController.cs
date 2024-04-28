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

            return RedirectToAction("Weddings");
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

            return RedirectToAction("Weddings");
        }
        else
        {
            return View("Index");
        }
    }

    // Dashboard : Dont forget to change it later relatively to exam 
    [HttpGet("Weddings")]
    public IActionResult Weddings()
    {

        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.NotLogIn = false;
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

        User userInDb = _context.Users.FirstOrDefault(a => a.UserId == userId);
        ViewBag.LoggedIn = userInDb?.FirstName; // Replace Name in case of project with First Name and Last Name :  Henda
        ViewBag.LoggedId = userInDb?.UserId; 

        
        
        //Display AllWeddings:
        ViewBag.AllWeddings = _context.Weddings.ToList();
        //All users taht reserv to wedding :
        ViewBag.AllUsers = _context.Users.Include(a => a.CreatedReservations).ThenInclude(d => d.Wedding).ToList();

        return View();
    }

    // Other routes in exam
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }


    // Plan a Wedding : 
    [HttpGet("weddings/new")]
    public IActionResult PlanWedding()
    {

        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.NotLogIn = false;
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

        User userInDb = _context.Users.FirstOrDefault(a => a.UserId == userId);
        ViewBag.LoggedIn = userInDb?.FirstName; // Replace Name in case of project with First Name and Last Name :  Henda
        return View();
    }

    [HttpPost("wedding/add")]
    public IActionResult WeddingAdd(Wedding newWedding, int WeddingId)
    {
        ViewBag.NotLogIn = false;
        if (ModelState.IsValid)
        {
            // Check that date is in the future :
            if( (DateTime.Now < newWedding.Date)  )
            
                {
            newWedding.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newWedding);
            _context.SaveChanges();
            return RedirectToAction("PlanWedding");

            }
            else {
                ModelState.AddModelError("Date", "Should be in the Future"); 
                return View("PlanWedding");

            }
            }

        else
        {
            return View("PlanWedding");
        }
    }

[HttpGet("weddings/{WeddingId}")]
    public IActionResult OneWedding(int WeddingId)
    {

        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.NotLogIn = false;
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

        User userInDb = _context.Users.FirstOrDefault(a => a.UserId == userId);
        ViewBag.LoggedIn = userInDb?.FirstName; // Replace Name in case of project with First Name and Last Name :  Henda
        //
        ViewBag.AllWeddings = _context.Weddings.Where(w => w.WeddingId == WeddingId).ToList();
       // List of users who reserve
        ViewBag.AllUsers = _context.Users.Include(a => a.CreatedReservations).ThenInclude(d => d.Wedding).ToList();
        return View();
    }



    [HttpGet("delete/{WeddingId}")]
    
    public IActionResult Delete(int WeddingId)
    {
        Wedding RetrievedWedding = _context.Weddings
        .SingleOrDefault(d => d.WeddingId == WeddingId);
        _context.Weddings.Remove(RetrievedWedding);
        _context.SaveChanges();
    
        return RedirectToAction ("Weddings");
    }


[HttpPost("reserve")]
public IActionResult AddReservation(int weddingId)
{
    if (HttpContext.Session.GetInt32("UserId") == null)
    {
        return RedirectToAction("Index");
    }

    int userId = HttpContext.Session.GetInt32("UserId").Value;

    // Check if the user has already reserved the wedding
    Reservation existingReservation = _context.Reservations
        .FirstOrDefault(r => r.WeddingId == weddingId && r.UserId == userId);

    if (existingReservation == null)
    {
        // User hasn't reserved the wedding, so create a new reservation
        Reservation newReservation = new Reservation
        {
            WeddingId = weddingId,
            UserId = userId
        };

        _context.Add(newReservation);
    }
    else
    {
        // User has already reserved the wedding, so delete the reservation
        _context.Remove(existingReservation);
    }

    _context.SaveChanges();

    return RedirectToAction("Weddings");
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
