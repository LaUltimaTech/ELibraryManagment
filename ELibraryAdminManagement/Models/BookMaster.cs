using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    [Table("BookMaster")]
    public class BookMaster
    {
        [Key]
        public int Id { get; set; }

        [Required] // must be provided
        [StringLength(255)]
        public string Name { get; set; }

        [Required] // must be provided
        public int BookCategoryId { get; set; }

        // optional fields
        public string? CoverPagePath { get; set; }
        public string? PdfPath { get; set; }
        public string? AudioPath { get; set; }

        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string? ExtractedText { get; set; }

        [ForeignKey("BookCategoryId")]
        public BookCategoryMaster BookCategory { get; set; }
    }
}
