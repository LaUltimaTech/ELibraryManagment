using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("StandardMaster")]
    public class StandardMaster
    {
        [Key]
        public int Id { get; set; }

        public string StandardName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
