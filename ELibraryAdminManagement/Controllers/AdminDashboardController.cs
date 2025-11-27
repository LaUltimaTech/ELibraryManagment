using Microsoft.AspNetCore.Mvc;

public class AdminDashboardController : Controller
{
    public IActionResult Index()
    {
        // ONLY ADMIN ACCESS
        //if (HttpContext.Session.GetString("Role") != "Admin")
        //    return RedirectToAction("AdminLogin", "Account");

        return View();
    }
}
