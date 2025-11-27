using System.ComponentModel.DataAnnotations;

namespace ELibraryAdminManagement.Models
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }

        public bool ChangePasswordEnabled { get; set; }

        public int BookDownloadLimit { get; set; }

        public string SchoolName { get; set; } = string.Empty;

        public DateTime LastUpdated { get; set; }
    }
}
