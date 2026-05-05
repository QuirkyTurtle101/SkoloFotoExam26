using System.Security.Cryptography;

namespace SkoloFotoExam26.Helpers
{
    /// <summary>
    /// Helper class for DRY of hashing.
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// Takes a string and returns the SHA256 hash of it.
        /// </summary>
        /// <param name="inputPassword">The string to be hashed.</param>
        /// <returns>The SHA256 hash of the input string.</returns>
        public static byte[] HashPassword(string inputPassword)
        {
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(inputPassword);
            return SHA256.HashData(inputBytes);
        }
    }
}
