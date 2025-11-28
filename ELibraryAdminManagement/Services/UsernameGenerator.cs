using ELibraryAdminManagement.Data;
using ELibraryAdminManagement.Models;
using ELibraryAdminManagement.Data;
using System.Text.RegularExpressions;

namespace ELibrary.Services
{
    public class UsernameGenerator : IUsernameGenerator
    {
        private readonly AppDbContext _db;

        public UsernameGenerator(AppDbContext db)
        {
            _db = db;
        }

        public string Generate(string fullName, string mobile)
        {
            var letters = Regex.Replace(fullName.ToUpper(), "[^A-Z]", "");
            var first = letters.Length >= 4 ? letters.Substring(0, 4) : letters.PadRight(4, 'X');

            var digits = Regex.Replace(mobile, "[^0-9]", "");
            var last = digits.Length >= 4 ? digits.Substring(digits.Length - 4) : digits.PadLeft(4, '0');

            var username = first + last;
            var baseUsername = username;
            int i = 1;

            while (_db.Users.Any(u => u.Username == username))
            {
                username = baseUsername + i;
                i++;
            }

            return username;
        }
    }
}
