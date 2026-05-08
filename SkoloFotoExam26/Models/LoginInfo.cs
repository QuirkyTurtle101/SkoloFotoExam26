using SkoloFotoExam26.Helpers;

namespace SkoloFotoExam26.Models
{
    /// <summary>
    /// Model for handling login info pulled from the database. Used for LoginRepo.
    /// </summary>
    public class LoginInfo
    {
        public string Email { get; }
        public byte[] PasswordHash { get; }
        public UserType TheUserType { get; }

        public LoginInfo(string email, byte[] passwordHash, UserType theUserType)
        {
            Email = email;
            PasswordHash = passwordHash;
            TheUserType = theUserType;
        }

        public LoginInfo(string email, string passwordHash, UserType theUserType)
        {
            Email = email;
            PasswordHash = HashHelper.HashPassword(passwordHash);
            TheUserType = theUserType;
        }
    }
}
