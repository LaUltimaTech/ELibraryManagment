using ELibraryAdminManagement.Data; // Change to your namespace
using Microsoft.AspNetCore.Mvc;

public class UserMasterController : Controller
{
    private readonly AppDbContext _context;

    public UserMasterController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Login Page
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login Check
    [HttpPost]
    public IActionResult Login(string UserName, string Password)
    {
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
        {
            ViewBag.ErrorMessage = "Please enter Username & Password";
            return View();
        }

        var user = _context.UserMasters
            .FirstOrDefault(u => u.UserName == UserName && u.Password == Password);

        if (user != null)
        {
            // Login success → Redirect to Dashboard
            return RedirectToAction("Index", "AdminDashboard");
        }
        else
        {
            ViewBag.ErrorMessage = "Invalid Username or Password";
            return View();
        }
    }
}
