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

        /// <summary>
        /// Helper function for checking that two hashes are equivalent.
        /// </summary>
        /// <param name="hash1">First hash to check.</param>
        /// <param name="hash2">Second hash to check.</param>
        /// <returns>A boolean indicating whether the two input hashes are equivalent</returns>
        public static bool CheckHashes(byte[] hash1, byte[] hash2)
        {
            bool result = true;
            for (int i = 0; i < 32; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
