using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("BookCategoryMaster")]
    public class BookCategoryMaster
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoPath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
