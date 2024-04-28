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
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {

        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.NotLogIn = false;
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

        User userInDb = _context.Users.FirstOrDefault(a => a.UserId == userId);
        ViewBag.LoggedIn = userInDb?.Name; // Replace Name in case of project with First Name and Last Name :  Henda

        // All Songs of all users 
        ViewBag.AllSongs = _context.Songs.ToList();
        // only songs related to mu user 
        ViewBag.AllSongsOfOneUser = userInDb?.CreatedSongs;


        return View();
    }



    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet("AddSong")]
    public IActionResult AddSong()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }
        // 3 lines juste to get : Hello, Derbali Henda! in HTML
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        User userInDb = _context.Users.FirstOrDefault(a => a.UserId == userId);
        ViewBag.LoggedIn = userInDb.Name;

        ViewBag.NotLogIn = false;
        return View();
    }

    [HttpPost("song/add")]
    public IActionResult CreateSong(Song newSong)
    {
        ViewBag.NotLogIn = false;
        if (ModelState.IsValid)
        {
            newSong.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newSong);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");

        }
        else
        {
            return View("AddSong");
        }
    }
    [HttpGet("AllSongs")]
    public IActionResult AllSongs()
    {

        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.NotLogIn = false;
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

        User userInDb = _context.Users.FirstOrDefault(a => a.UserId == userId);
        ViewBag.LoggedIn = userInDb?.Name; // Replace Name in case of project with First Name and Last Name

        // All Songs of all users 
        ViewBag.AllSongs = _context.Songs.ToList();
        ViewBag.AllSongs = _context.Songs.Include(a => a.Creator).ToList();


        // All Likes :
        ViewBag.AllLikes = _context.Likes.Include(l => l.Magazine).ToList();


        return View();
    }

    [HttpGet("songs/{SongId}")]
    public IActionResult DisplaySong(int SongId)
    {
        ViewBag.SingleSong = _context.Songs.Include(a => a.Creator).Where(p => p.SongId == SongId).ToList();


        //New to add in this case  :
        //ViewBag.SongId = SongId; 
        return View();
    }


    [HttpPost("AddLike")]
    public IActionResult AddLike(int SongId)
    {
        // Ensure the user is authenticated
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index");
        }

        // Create a new Like with the correct UserId and SongId
        Like newLike = new Like
        {
            UserId = HttpContext.Session.GetInt32("UserId").Value,
            SongId = SongId
        };

        if (ModelState.IsValid)
        {
            _context.Add(newLike);
            _context.SaveChanges();
        }

        // Redirect back to the song details page
        return RedirectToAction("DisplaySong", new { SongId = SongId });
    }
















    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}