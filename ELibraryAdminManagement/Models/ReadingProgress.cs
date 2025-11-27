using System.ComponentModel.DataAnnotations.Schema;

namespace ELibraryAdminManagement.Models
{
    public class ReadingProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int EBookId { get; set; }
        public int LastPage { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("EBookId")]
        public BookMaster Book { get; set; }
    }
}
