using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("TeacherMaster")]
    public class TeacherMaster
    {
        [Key]
        public int Id { get; set; }

        public string TeacherName { get; set; }

        // Foreign Key
        public int SchoolId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string State { get; set; }

        public string MobileNumber { get; set; }

        public string WhatsappNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        //ForeignKey
        [ForeignKey("SchoolId")]
        public SchoolMaster School { get; set; }
    }
}
