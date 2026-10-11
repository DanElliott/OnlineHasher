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
                return HashResult.Failure("Password is required.");
            }

            byte[] saltBytes = string.IsNullOrWhiteSpace(salt) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(salt);

            byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password), saltBytes, Iterations, HashAlgorithmName.SHA1, HashLength);

            return HashResult.Success(Convert.ToHexString(hashBytes));
        }
    }

    public sealed record HashResult(string Hash, string Error)
    {
        public bool IsSuccess => Error is null;

        public static HashResult Success(string hash) => new(hash, null);

        public static HashResult Failure(string error) => new(null, error);
    }
}
