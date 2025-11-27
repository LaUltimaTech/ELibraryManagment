using Microsoft.AspNetCore.Mvc;

public class UserDashboardController : Controller
{
    public IActionResult Index()
    {
        var role = HttpContext.Session.GetString("Role");

        // ONLY Student or Teacher
        if (role != "Student" && role != "Teacher")
            return RedirectToAction("SchoolCode", "Account");

        return View();
    }
}
