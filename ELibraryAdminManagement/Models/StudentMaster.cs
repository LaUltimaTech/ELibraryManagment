using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("StudentMaster")]
    public class StudentMaster
    {
        [Key]
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string AdmissionNo { get; set; } 

        // Foreign Keys
        public int SchoolId { get; set; }
        public int StandardId { get; set; }
        public int DivisionId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string District { get; set; } 

        public string State { get; set; }

        public string FatherName { get; set; }

        public string FatherMobileNumber { get; set; } 

        public string FatherWhatsappNumber { get; set; }

        public string MotherName { get; set; }

        public string MotherMobileNumber { get; set; }

        public string MotherWhatsappNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        //ForeignKey

        [ForeignKey("SchoolId")]
        public SchoolMaster School { get; set; }

        [ForeignKey("StandardId")]
        public StandardMaster Standard { get; set; }

        [ForeignKey("DivisionId")]
        public DivisionMaster Division { get; set; }
    }
}
