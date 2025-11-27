using ELibraryAdminManagement.Data;
using ELibraryAdminManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELibraryAdminManagement.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class DivisionMasterController : Controller
    {
        private readonly AppDbContext _context;

        public DivisionMasterController(AppDbContext context)
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
            var data = _context.DivisionMasters
                    .OrderBy(x => x.Id)
                    .ToList();

            return Json(data);
        }

        // CREATE OR UPDATE
        [HttpPost]
        public JsonResult Save(DivisionMaster model)
        {
            if (model.Id == 0)
            {
                model.CreatedDate = DateTime.Now;
                _context.DivisionMasters.Add(model);
            }
            else
            {
                var item = _context.DivisionMasters.Find(model.Id);

                if (item != null)
                {
                    item.DivisionName = model.DivisionName;
                }
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        // DELETE
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = _context.DivisionMasters.Find(id);

            if (data != null)
            {
                _context.DivisionMasters.Remove(data);
                _context.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}
