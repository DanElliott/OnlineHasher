using System;
using System.Security.Cryptography;
using System.Text;

namespace OnlineHasher
{
    public static class PasswordHasher
    {
        // Fixed parameters are part of the output contract: changing them changes every hash users already have.
        private const int Iterations = 1000;
        private const int HashLength = 256 / 8;

        /// <summary>
        /// PBKDF2 (HMAC-SHA1, 1000 iterations, 32 bytes) as uppercase hex.
        /// A null, empty or whitespace salt is treated as no salt.
        /// </summary>
        public static string Hash(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is required.", nameof(password));
            }

            byte[] saltBytes = string.IsNullOrWhiteSpace(salt) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(salt);

            byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password), saltBytes, Iterations, HashAlgorithmName.SHA1, HashLength);

            return Convert.ToHexString(hashBytes);
        }
    }
}
