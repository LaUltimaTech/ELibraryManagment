using Microsoft.AspNetCore.Mvc;
using ELibraryAdminManagement.Models;
using ELibraryAdminManagement.Data;
using Microsoft.AspNetCore.Authorization;

namespace ELibraryAdminManagement.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class BookCategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookCategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET ALL ----------------------------------------------------------
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.BookCategoryMasters
                .OrderBy(x => x.Id)
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    logoPath = x.LogoPath
                })
                .ToList();

            return Json(data);
        }

        // GET BY ID --------------------------------------------------------
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var data = _context.BookCategoryMasters.FirstOrDefault(x => x.Id == id);
            if (data == null)
                return Json(new { success = false });

            return Json(new
            {
                id = data.Id,
                name = data.Name,
                logoPath = data.LogoPath
            });
        }

        // CREATE -----------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile logo, string name)
        {
            string logoPath = null;

            if (logo != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads/category");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(logo.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(stream);
                }

                logoPath = "/uploads/category/" + fileName;
            }

            var model = new BookCategoryMaster
            {
                Name = name,
                LogoPath = logoPath,
                CreatedDate = DateTime.Now
            };

            _context.BookCategoryMasters.Add(model);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // UPDATE -----------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Update(int id, string name, string? logoPath, IFormFile? logo)
        {
            var data = _context.BookCategoryMasters.FirstOrDefault(x => x.Id == id);
            if (data == null)
                return Json(new { success = false });

            data.Name = name;

            // IF NEW FILE IS UPLOADED
            if (logo != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads/category");
                Directory.CreateDirectory(folder);

                string newFileName = Guid.NewGuid() + Path.GetExtension(logo.FileName);
                string newFilePath = Path.Combine(folder, newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await logo.CopyToAsync(stream);
                }

                // DELETE OLD FILE
                if (!string.IsNullOrEmpty(data.LogoPath))
                {
                    string oldFile = Path.Combine(_env.WebRootPath, data.LogoPath.TrimStart('/'));

                    if (System.IO.File.Exists(oldFile))
                        System.IO.File.Delete(oldFile);
                }

                data.LogoPath = "/uploads/category/" + newFileName;
            }
            else
            {
                // NO NEW FILE → KEEP OLD FILE
                data.LogoPath = logoPath;
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        // DELETE -----------------------------------------------------------
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _context.BookCategoryMasters.FirstOrDefault(x => x.Id == id);
            if (data == null)
                return Json(new { success = false });

            // DELETE LOGO FILE FROM SERVER
            if (!string.IsNullOrEmpty(data.LogoPath))
            {
                string file = Path.Combine(_env.WebRootPath, data.LogoPath.TrimStart('/'));
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }

            _context.BookCategoryMasters.Remove(data);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}
