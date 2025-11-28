namespace ELibrary.Services
{
    public interface IUsernameGenerator
    {
        string Generate(string fullName, string mobile);
    }
}
