using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("SchoolMaster")]
    public class SchoolMaster
    {
        [Key]
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string State { get; set; }

        public string OfficeNumber { get; set; }

        public string WhatsappNumber { get; set; }

        public string EmailId { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNumber { get; set; }

        public string Website { get; set; }

        public string? LogoPath { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}