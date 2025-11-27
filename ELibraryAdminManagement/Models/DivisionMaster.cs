using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("DivisionMaster")]
    public class DivisionMaster
    {
        [Key]
        public int Id { get; set; }

        public string DivisionName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
