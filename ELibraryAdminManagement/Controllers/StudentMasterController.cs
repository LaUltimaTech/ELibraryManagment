using ELibraryAdminManagement.Data;
using ELibraryAdminManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELibraryAdminManagement.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class StudentMasterController : Controller
    {
        private readonly AppDbContext _context;

        public StudentMasterController(AppDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index()
        {
            ViewBag.Schools = _context.SchoolMasters.ToList();
            ViewBag.Standards = _context.StandardMasters.ToList();
            ViewBag.Divisions = _context.DivisionMasters.ToList();

            return View();
        }


        // ================= LIST DATA AJAX =================
        public async Task<IActionResult> GetList()
        {
            var list = await _context.StudentMasters
                .Include(x => x.School)
                .Include(x => x.Standard)
                .Include(x => x.Division)
                .ToListAsync();

            return Json(new { data = list });
        }


        // ================= CREATE / UPDATE =================
        //[HttpPost]
        //public async Task<IActionResult> Save(StudentMaster model)
        //{
        //    if (model.StudentId == 0)
        //    {
        //        model.CreatedDate = DateTime.Now;
        //        _context.StudentMasters.Add(model);
        //    }
        //    else
        //    {
        //        _context.StudentMasters.Update(model);
        //    }

        //    await _context.SaveChangesAsync();
        //    return Json(new { success = true });
        //}

        [HttpPost]
        public async Task<IActionResult> Save(StudentMaster model)
        {
            if (model.StudentId == 0)
            {
                // INSERT
                model.CreatedDate = DateTime.Now;
                _context.StudentMasters.Add(model);
            }
            else
            {
                // UPDATE (Important: Only update changed fields)
                var existing = await _context.StudentMasters.FindAsync(model.StudentId);
                if (existing == null)
                    return Json(new { success = false, message = "Record not found" });

                existing.StudentName = model.StudentName;
                existing.AdmissionNo = model.AdmissionNo;
                existing.DateOfBirth = model.DateOfBirth;
                existing.EmailId = model.EmailId;
                existing.SchoolId = model.SchoolId;
                existing.StandardId = model.StandardId;
                existing.DivisionId = model.DivisionId;
                existing.Address = model.Address;
                existing.City = model.City;
                existing.District = model.District;
                existing.State = model.State;
                existing.FatherName = model.FatherName;
                existing.FatherMobileNumber = model.FatherMobileNumber;
                existing.FatherWhatsappNumber = model.FatherWhatsappNumber;
                existing.MotherName = model.MotherName;
                existing.MotherMobileNumber = model.MotherMobileNumber;
                existing.MotherWhatsappNumber = model.MotherWhatsappNumber;

                // No need to set CreatedDate again
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }



        // ================= EDIT DATA (BY ID) =================
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _context.StudentMasters.FindAsync(id);
            return Json(data);
        }


        // ================= DELETE =================
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _context.StudentMasters.FindAsync(id);
            _context.StudentMasters.Remove(obj);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
