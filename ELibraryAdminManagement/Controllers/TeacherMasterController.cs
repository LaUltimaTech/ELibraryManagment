using ELibraryAdminManagement.Data;
using ELibraryAdminManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELibraryAdminManagement.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class TeacherMasterController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherMasterController(AppDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index()
        {
            ViewBag.Schools = _context.SchoolMasters.ToList();
            return View();
        }

        // ================= LIST =================
        public async Task<IActionResult> GetList()
        {
            var list = await _context.TeacherMasters
                .Include(x => x.School)
                .ToListAsync();

            return Json(new { data = list });
        }

        // ================= SAVE (CREATE + UPDATE) =================
        [HttpPost]
        public async Task<IActionResult> Save(TeacherMaster model)
        {
            try
            {
                if (model.SchoolId == 0)
                    return Json(new { success = false, message = "Please select School" });

                if (model.DateOfBirth == DateTime.MinValue)
                    return Json(new { success = false, message = "Invalid Date Of Birth" });

                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    _context.TeacherMasters.Add(model);
                }
                else
                {
                    var dbObj = await _context.TeacherMasters.FindAsync(model.Id);
                    if (dbObj == null)
                        return Json(new { success = false, message = "Record not found" });

                    dbObj.TeacherName = model.TeacherName;
                    dbObj.SchoolId = model.SchoolId;
                    dbObj.DateOfBirth = model.DateOfBirth;
                    dbObj.EmailId = model.EmailId;
                    dbObj.Address = model.Address;
                    dbObj.City = model.City;
                    dbObj.District = model.District;
                    dbObj.State = model.State;
                    dbObj.MobileNumber = model.MobileNumber;
                    dbObj.WhatsappNumber = model.WhatsappNumber;
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // ================= EDIT =================
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _context.TeacherMasters.FindAsync(id);
            return Json(data);
        }

        // ================= DELETE =================
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _context.TeacherMasters.FindAsync(id);
            if (obj == null)
                return Json(new { success = false });

            _context.TeacherMasters.Remove(obj);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
