using ELibraryAdminManagement.Data;
using ELibraryAdminManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELibraryAdminManagement.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class SchoolMasterController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SchoolMasterController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 🟦 GET ALL SCHOOLS (for table)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.SchoolMasters.ToListAsync();
            return Json(data);
        }

        // 🟩 SAVE (CREATE + UPDATE) with file upload
        [HttpPost]
        public async Task<IActionResult> Save()
        {
            var id = Convert.ToInt32(Request.Form["Id"]);
            var schoolName = Request.Form["SchoolName"];
            var schoolCode = Request.Form["SchoolCode"];
            var address = Request.Form["Address"];
            var city = Request.Form["City"];
            var district = Request.Form["District"];
            var state = Request.Form["State"];
            var officeNumber = Request.Form["OfficeNumber"];
            var whatsappNumber = Request.Form["WhatsappNumber"];
            var emailId = Request.Form["EmailId"];
            var contactPerson = Request.Form["ContactPerson"];
            var contactNumber = Request.Form["ContactNumber"];
            var website = Request.Form["Website"];

            // File
            var file = Request.Form.Files["LogoPath"];
            string fileName = null;

            if (file != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "SchoolLogos");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            SchoolMaster model;

            if (id == 0) // CREATE
            {
                model = new SchoolMaster
                {
                    SchoolName = schoolName,
                    SchoolCode = schoolCode,
                    Address = address,
                    City = city,
                    District = district,
                    State = state,
                    OfficeNumber = officeNumber,
                    WhatsappNumber = whatsappNumber,
                    EmailId = emailId,
                    ContactPerson = contactPerson,
                    ContactNumber = contactNumber,
                    Website = website,
                    LogoPath = fileName,
                    CreatedDate = DateTime.Now
                };

                _context.SchoolMasters.Add(model);
            }
            else // UPDATE
            {
                model = await _context.SchoolMasters.FindAsync(id);

                if (model == null)
                    return Json(new { success = false, message = "Record not found" });

                model.SchoolName = schoolName;
                model.SchoolCode = schoolCode;
                model.Address = address;
                model.City = city;
                model.District = district;
                model.State = state;
                model.OfficeNumber = officeNumber;
                model.WhatsappNumber = whatsappNumber;
                model.EmailId = emailId;
                model.ContactPerson = contactPerson;
                model.ContactNumber = contactNumber;
                model.Website = website;

                if (fileName != null)
                    model.LogoPath = fileName;

                _context.SchoolMasters.Update(model);
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // 🟨 GET BY ID (Load for update)
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _context.SchoolMasters.FindAsync(id);
            return Json(data);
        }

        // 🟥 DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.SchoolMasters.FindAsync(id);

            if (data == null)
                return Json(new { success = false });

            _context.SchoolMasters.Remove(data);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
