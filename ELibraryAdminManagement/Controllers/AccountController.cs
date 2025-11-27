using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ELibraryAdminManagement.Models;
using ELibraryAdminManagement.Data;

public class AccountController : Controller
{
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db)
    {
        _db = db;
    }

    // ============================================
    //                  ADMIN LOGIN
    // ============================================
    public IActionResult AdminLogin()
    {
        if (HttpContext.Session.GetString("Role") == "Admin")
            return RedirectToAction("Index", "AdminDashboard");

        return View();
    }

    [HttpPost]
    public IActionResult AdminLogin(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Please enter Username & Password!";
            return View();
        }

        var admin = _db.UserMasters.FirstOrDefault(x =>
            x.UserName == username &&
            x.Password == password &&
            x.Role == "Admin"
        );

        if (admin == null)
        {
            ViewBag.Error = "Invalid Username or Password!";
            return View();
        }

        // Store session
        HttpContext.Session.SetString("Role", "Admin");
        HttpContext.Session.SetInt32("UserId", admin.Id);

        return RedirectToAction("Index", "AdminDashboard");
    }



    // ============================================
    //           SCHOOL CODE LOGIN (STEP 1)
    // ============================================
    public IActionResult SchoolCode()
    {
        // If admin is already logged in → stop here
        if (HttpContext.Session.GetString("Role") == "Admin")
            return RedirectToAction("Index", "AdminDashboard");

        return View();
    }

    [HttpPost]
    public IActionResult SchoolCode(string schoolCode)
    {
        if (string.IsNullOrEmpty(schoolCode))
        {
            ViewBag.Error = "Please enter School Code!";
            return View();
        }

        var school = _db.SchoolMasters.FirstOrDefault(x => x.SchoolCode == schoolCode);

        if (school == null)
        {
            ViewBag.Error = "Invalid School Code!";
            return View();
        }

        return RedirectToAction("UserLogin", new { schoolId = school.Id });
    }



    // ============================================
    //           SCHOOL USER LOGIN (STEP 2)
    // ============================================
    public IActionResult UserLogin(int schoolId)
    {
        if (schoolId <= 0)
            return RedirectToAction("SchoolCode");

        ViewBag.SchoolId = schoolId;
        return View();
    }

    [HttpPost]
    public IActionResult UserLogin(int schoolId, string username, string password)
    {
        if (schoolId <= 0)
        {
            return RedirectToAction("SchoolCode");
        }

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Please enter Username & Password!";
            ViewBag.SchoolId = schoolId;
            return View();
        }

        var user = _db.UserMasters.FirstOrDefault(x =>
            x.UserName == username &&
            x.Password == password &&
            x.SchoolId == schoolId &&
            (x.Role == "Student" || x.Role == "Teacher")
        );

        if (user == null)
        {
            ViewBag.Error = "Invalid Username or Password!";
            ViewBag.SchoolId = schoolId;
            return View();
        }

        // Set session
        HttpContext.Session.SetString("Role", user.Role);
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetInt32("SchoolId", schoolId);

        return RedirectToAction("Index", "UserDashboard");
    }



    // ============================================
    //                     LOGOUT
    // ============================================
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("SchoolCode");
    }
}
