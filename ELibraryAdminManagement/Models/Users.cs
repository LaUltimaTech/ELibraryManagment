using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    
    [Table("tbl_Users")]
    public class Users
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
