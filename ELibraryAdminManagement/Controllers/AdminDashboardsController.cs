using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Controllers
{
    public class AdminDashboardsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
