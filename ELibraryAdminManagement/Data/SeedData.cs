using ELibraryAdminManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ELibraryAdminManagement.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // If admin already exists, skip
                if (context.UserMasters.Any(x => x.Role == "Admin"))
                    return;

                // Create Default Admin
                context.UserMasters.Add(new UserMaster
                {
                    UserName = "admin",
                    Password = "admin123",
                    Role = "Admin",
                    SchoolId = null,
                    CreatedDate = DateTime.Now
                });

                context.SaveChanges();
            }
        }
    }
}

