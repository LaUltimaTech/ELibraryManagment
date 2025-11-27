using ELibraryAdminManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELibraryAdminManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<SchoolMaster> SchoolMasters { get; set; }
        public DbSet<StandardMaster> StandardMasters { get; set; }
        public DbSet<DivisionMaster> DivisionMasters { get; set; }
        public DbSet<BookMaster> BookMasters { get; set; }
        public DbSet<BookCategoryMaster> BookCategoryMasters { get; set; }
        public DbSet<StudentMaster> StudentMasters { get; set; }
        public DbSet<TeacherMaster> TeacherMasters { get; set; }

        //public DbSet<Book> Books { get; set; }
        //public DbSet<BookCategory> BookCategory { get; set; }

        //public DbSet<ReadingProgress> ReadingProgress { get; set; }
    }
}
