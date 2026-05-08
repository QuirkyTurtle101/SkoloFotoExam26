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

        /// <summary>
        /// Constructor for pulling information from the repository and using it.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="passwordHash">The user's hashed password.</param>
        /// <param name="theUserType">The UserType of the user.</param>
        public LoginInfo(string email, byte[] passwordHash, UserType theUserType)
        {
            Email = email;
            PasswordHash = passwordHash;
            TheUserType = theUserType;
        }

        /// <summary>
        /// Constructor for receiving information from the HTML form and sending it to the repository.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's plaintext password as typed into the form.</param>
        /// <param name="theUserType">The UserType of the user.</param>
        public LoginInfo(string email, string password, UserType theUserType)
        {
            Email = email;
            PasswordHash = HashHelper.HashPassword(password);
            TheUserType = theUserType;
        }
    }
}
