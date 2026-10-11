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
        /// A null or empty password fails; whitespace is a valid password.
        /// </summary>
        public static HashResult Hash(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                return new HashResult.Failure("Password is required.");
            }

            byte[] saltBytes = string.IsNullOrWhiteSpace(salt) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(salt);

            byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password), saltBytes, Iterations, HashAlgorithmName.SHA1, HashLength);

            return new HashResult.Success(Convert.ToHexString(hashBytes));
        }
    }

    /// <summary>Closed hierarchy: a result is exactly a <see cref="Success"/> or a <see cref="Failure"/>.</summary>
    public abstract record HashResult
    {
        private HashResult() { }

        public sealed record Success(string Hash) : HashResult;

        public sealed record Failure(string Error) : HashResult;
    }
}
