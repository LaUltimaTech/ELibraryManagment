using Microsoft.AspNetCore.Mvc;

public class UserDashboardController : Controller
{
    public IActionResult Index()
    {
        // Session login role 
        var role = HttpContext.Session.GetString("Role");   // Student / Teacher
        var school = HttpContext.Session.GetString("SchoolName");

        ViewBag.Role = role;
        ViewBag.School = school;

        return View();   // ← Default = Index.cshtml (Perfect)
    }
}
