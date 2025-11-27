using ELibraryAdminManagement.Data;
using ELibraryAdminManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ELibraryAdminManagement.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class BookMasterController : Controller
    {
        private readonly AppDbContext _context;

        public BookMasterController(AppDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // 1. INDEX
        // =====================================================
        public async Task<IActionResult> Index()
        {
            var books = await _context.BookMasters
                .Include(b => b.BookCategory)
                .ToListAsync();

            ViewBag.BookCategoryId = new SelectList(_context.BookCategoryMasters, "Id", "Name");

            return View(books);
        }

        // =====================================================
        // 2. CREATE (AJAX)
        // =====================================================
        [HttpPost]
        public async Task<IActionResult> Create(BookMaster bookMaster, IFormFile pdfFile, IFormFile coverFile, IFormFile audioFile)
        {
            if (string.IsNullOrWhiteSpace(bookMaster.Name) || bookMaster.BookCategoryId <= 0)
            {
                return Json(new { success = false, message = "Name and Category are required." });
            }

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Books");

            if (pdfFile != null && pdfFile.Length > 0)
            {
                var pdfPath = Path.Combine(rootPath, "Pdfs", pdfFile.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(pdfPath));
                using var stream = new FileStream(pdfPath, FileMode.Create);
                await pdfFile.CopyToAsync(stream);
                bookMaster.PdfPath = $"Books/Pdfs/{pdfFile.FileName}";
            }

            if (coverFile != null && coverFile.Length > 0)
            {
                var coverPath = Path.Combine(rootPath, "Covers", coverFile.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(coverPath));
                using var stream = new FileStream(coverPath, FileMode.Create);
                await coverFile.CopyToAsync(stream);
                bookMaster.CoverPagePath = $"Books/Covers/{coverFile.FileName}";
            }

            if (audioFile != null && audioFile.Length > 0)
            {
                var audioPath = Path.Combine(rootPath, "Audios", audioFile.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(audioPath));
                using var stream = new FileStream(audioPath, FileMode.Create);
                await audioFile.CopyToAsync(stream);
                bookMaster.AudioPath = $"Books/Audios/{audioFile.FileName}";
            }

            bookMaster.CreatedDate = DateTime.Now;

            _context.Add(bookMaster);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Book saved successfully!" });
        }

        // =====================================================
        // 3. DELETE (AJAX)
        // =====================================================
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.BookMasters.FindAsync(id);
            if (book != null)
            {
                _context.BookMasters.Remove(book);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Book deleted successfully!" });
            }

            return Json(new { success = false, message = "Book not found." });
        }

        // =====================================================
        // 4. EDIT (GET)
        // =====================================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.BookMasters.FindAsync(id);
            if (book == null) return NotFound();

            ViewBag.BookCategoryId = new SelectList(_context.BookCategoryMasters, "Id", "Name", book.BookCategoryId);

            return View(book);
        }

        // =====================================================
        // 5. EDIT (POST)
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookMaster bookMaster, IFormFile pdfFile, IFormFile coverFile, IFormFile audioFile)
        {
            if (id != bookMaster.Id) return NotFound();

            if (string.IsNullOrWhiteSpace(bookMaster.Name) || bookMaster.BookCategoryId <= 0)
            {
                ViewBag.BookCategoryId = new SelectList(_context.BookCategoryMasters, "Id", "Name", bookMaster.BookCategoryId);
                return View(bookMaster);
            }

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Books");

            if (pdfFile != null && pdfFile.Length > 0)
            {
                var pdfPath = Path.Combine(rootPath, "Pdfs", pdfFile.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(pdfPath));
                using var stream = new FileStream(pdfPath, FileMode.Create);
                await pdfFile.CopyToAsync(stream);
                bookMaster.PdfPath = $"Books/Pdfs/{pdfFile.FileName}";
            }

            if (coverFile != null && coverFile.Length > 0)
            {
                var coverPath = Path.Combine(rootPath, "Covers", coverFile.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(coverPath));
                using var stream = new FileStream(coverPath, FileMode.Create);
                await coverFile.CopyToAsync(stream);
                bookMaster.CoverPagePath = $"Books/Covers/{coverFile.FileName}";
            }

            if (audioFile != null && audioFile.Length > 0)
            {
                var audioPath = Path.Combine(rootPath, "Audios", audioFile.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(audioPath));
                using var stream = new FileStream(audioPath, FileMode.Create);
                await audioFile.CopyToAsync(stream);
                bookMaster.AudioPath = $"Books/Audios/{audioFile.FileName}";
            }

            _context.Update(bookMaster);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // 6. EXPORT EXCEL (NPOI)
        // =====================================================
        public async Task<IActionResult> Export()
        {
            var books = await _context.BookMasters.ToListAsync();

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Books");

            IRow header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("Id");
            header.CreateCell(1).SetCellValue("Name");
            header.CreateCell(2).SetCellValue("BookCategoryId");
            header.CreateCell(3).SetCellValue("CoverPagePath");
            header.CreateCell(4).SetCellValue("PdfPath");
            header.CreateCell(5).SetCellValue("AudioPath");
            header.CreateCell(6).SetCellValue("CreatedDate");

            int rowNumber = 1;
            foreach (var b in books)
            {
                IRow row = sheet.CreateRow(rowNumber++);
                row.CreateCell(0).SetCellValue(b.Id);
                row.CreateCell(1).SetCellValue(b.Name);
                row.CreateCell(2).SetCellValue(b.BookCategoryId);
                row.CreateCell(3).SetCellValue(b.CoverPagePath);
                row.CreateCell(4).SetCellValue(b.PdfPath);
                row.CreateCell(5).SetCellValue(b.AudioPath);
                row.CreateCell(6).SetCellValue(b.CreatedDate.ToString());
            }

            using var stream = new MemoryStream();
            workbook.Write(stream);

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "BookMaster.xlsx");
        }


        // =====================================================
        // 7. IMPORT EXCEL (NPOI)
        // =====================================================
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
            {
                return Json(new { success = false, message = "Please upload a valid Excel file." });
            }

            try
            {
                using var stream = new MemoryStream();
                await excelFile.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    IRow current = sheet.GetRow(row);
                    if (current == null) continue;

                    string name = current.GetCell(1)?.ToString();
                    string categoryIdText = current.GetCell(2)?.ToString();

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(categoryIdText))
                        continue;

                    int categoryId = int.Parse(categoryIdText);

                    bool exists = _context.BookMasters.Any(x =>
                        x.Name == name && x.BookCategoryId == categoryId);

                    if (exists) continue;

                    var book = new BookMaster
                    {
                        Name = name,
                        BookCategoryId = categoryId,
                        CoverPagePath = current.GetCell(3)?.ToString(),
                        PdfPath = current.GetCell(4)?.ToString(),
                        AudioPath = current.GetCell(5)?.ToString(),
                        CreatedDate = DateTime.Now
                    };

                    _context.BookMasters.Add(book);
                }

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Excel imported successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Import failed: " + ex.Message });
            }
        }

    }
}
