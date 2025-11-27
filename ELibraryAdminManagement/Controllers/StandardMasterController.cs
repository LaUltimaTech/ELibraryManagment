using ELibraryAdminManagement.Data;
using ELibraryAdminManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELibraryAdminManagement.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class StandardMasterController : Controller
    {
        private readonly AppDbContext _context;

        public StandardMasterController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET ALL
        public JsonResult GetAll()
        {
            var data = _context.StandardMasters
                    .OrderBy(x => x.Id)
                    .ToList();

            return Json(data);
        }

        // CREATE OR UPDATE
        [HttpPost]
        public JsonResult Save(StandardMaster model)
        {
            if (model.Id == 0)
            {
                model.CreatedDate = DateTime.Now;
                _context.StandardMasters.Add(model);
            }
            else
            {
                var existing = _context.StandardMasters.Find(model.Id);
                if (existing != null)
                {
                    existing.StandardName = model.StandardName;
                }
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        // DELETE
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = _context.StandardMasters.Find(id);
            if (data != null)
            {
                _context.StandardMasters.Remove(data);
                _context.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}
