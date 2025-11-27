using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("UserMaster")]
    public class UserMaster
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin", "Student", "Teacher"

        public int? SchoolId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}